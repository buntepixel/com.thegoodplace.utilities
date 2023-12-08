using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;



namespace TGP.Utilities {
	public class LoggerManager : BaseMonoBehaviour {
		[SerializeField]
		public LogLevel _LogLevel;
		[SerializeField]
		int LogFilesToKeep = 10;
		[SerializeField]
		string Filepath;
		[SerializeField]
		TMP_Text ErrorTextfield;
		[Flags]
		public enum LogLevel {
			none = 0,
			Errors = 1 << 0,
			Warnings = 1 << 1,
			Information = 1 << 2,
			Exeption = 1 << 3
		}
		Logger logger;
		protected override void Awake() {
			base.Awake();
			logger = new Logger(LogFilesToKeep);
			Logger.CreateAppLogFile();
			if (debug)
				logger.debug = debug;
			Filepath = Logger.CurrLogFilePath;
		}

		private void OnEnable() {
			Application.logMessageReceived += LogCallbacksToFile;
		}
		private void OnDisable() {
			Application.logMessageReceived -= LogCallbacksToFile;
		}
		private void OnDestroy() {
			Logger.WriteLogEntry("-----------------App Closed--------------\n\n", LogType.Warning);
		}

		void LogCallbacksToFile(string condition, string stacktrace, LogType logtype) {
			switch (logtype) {
				case LogType.Error:
					LogToWindow(condition, stacktrace);
					if (_LogLevel.HasFlag(LogLevel.Errors))
						LogErrors(condition, stacktrace, logtype);
					break;
				case LogType.Assert:
					break;
				case LogType.Warning:
					if (_LogLevel.HasFlag(LogLevel.Warnings))
						LogWarnings(condition, stacktrace, logtype);
					break;
				case LogType.Log:
					if (_LogLevel.HasFlag(LogLevel.Information))
						LogInformation(condition, stacktrace, logtype);
					break;
				case LogType.Exception:
					LogToWindow(condition, stacktrace);
					if (_LogLevel.HasFlag(LogLevel.Exeption))
						LogException(condition, stacktrace, logtype);
					break;
				default:
					break;
			}
		}
		void LogToWindow(string condition, string stacktrace) {
			if (ErrorTextfield == null)
				return;
			CanvasGroup cg = ErrorTextfield.GetComponentInParent<CanvasGroup>();
			if(cg!= null) {
				cg.EnableInputVisibility(true);
			}
			ErrorTextfield.text = string.Concat(condition, "\n--------------\n", stacktrace);
		}
		void LogErrors(string condition, string stacktrace, LogType logtype) {
			Logger.WriteLogEntry(condition, logtype, stacktrace);
		}
		void LogWarnings(string condition, string stacktrace, LogType logtype) {
			Logger.WriteLogEntry(condition, logtype);
		}
		void LogInformation(string condition, string stacktrace, LogType logtype) {
			Logger.WriteLogEntry(condition, logtype);
		}
		void LogException(string condition, string stacktrace, LogType logtype) {
			Logger.WriteLogEntry(condition, logtype, stacktrace);
		}
		public void SetBit(int bitNr) {
			byte tmp = (byte)_LogLevel;
			tmp = (byte)(tmp | (1 << bitNr));
			_LogLevel = (LogLevel)tmp;
		}
		protected void UnSetBit(int bitNr) {
			byte tmp = (byte)_LogLevel;
			tmp = (byte)(tmp & ~(1 << bitNr));
			_LogLevel = (LogLevel)tmp;
		}
	}
	public class Logger {
		static int counter;
		public bool debug;
		public static string CurrLogFilePath { get; private set; }
		static ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
		public Logger(int filesToKeep) {
			CurrLogFilePath = string.Concat(Application.persistentDataPath, "/_", DateTime.Now.ToString("yyMMdd"), "_Logfile", ".txt");
			CheckIfDirExists(CurrLogFilePath);
			DeleteOldFiles(filesToKeep);
		}
		void CheckIfDirExists(string dir) {
			if (debug)
				Debug.LogFormat($"dirIn: {dir}");
			string dirOnly = Path.GetDirectoryName(dir);


			if (!Directory.Exists(dirOnly))
				Directory.CreateDirectory(dirOnly);
		}
		public static void CreateAppLogFile() {
			counter = 0;
			StreamWriter currLogFile;
			if (!File.Exists(CurrLogFilePath)) {
				//todo:make sure old files get deleted
				using (currLogFile = File.CreateText(CurrLogFilePath)) {
					currLogFile.WriteLine(String.Concat("Logfile created:", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo)));
				}
			}
		}
		void DeleteOldFiles(int keep) {
			foreach (var fi in new DirectoryInfo(Application.persistentDataPath).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(keep))
				fi.Delete();
		}
		public static void WriteLogEntry(string inputString, LogType logType, string stacktracke = "") {
			lock_.EnterWriteLock();
			try {
				using (StreamWriter currLogFile = File.AppendText(CurrLogFilePath)) {
					currLogFile.WriteLine(string.Concat("\nEntry Nr:::", counter, "::: LogType: ", logType.ToString(), "  LogTime: ", DateTime.Now.ToString("HH:mm:ss  ffff")));
					currLogFile.WriteLine(inputString);
					if (!string.IsNullOrEmpty(stacktracke))
						currLogFile.WriteLine(string.Concat(":::Stacktrace::::\n", stacktracke));
					counter++;
				}
			} catch (Exception ex) {
				Debug.LogWarningFormat("error whilewriting Log   Message:{0}", ex);
			} finally {
				lock_.ExitWriteLock();
			}
		}
	}

}
