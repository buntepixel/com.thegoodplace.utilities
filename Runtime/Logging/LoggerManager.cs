using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading;
using System.Globalization;



namespace TGP.Utilities {
	public class LoggerManager : BaseMonoBehaviour {
		[SerializeField]
		public LogLevel _LogLevel;
		[SerializeField]
		string Filepath;
	
		public enum LogLevel {
			none = 0,
			Errors = 1,
			Warnings = 2,
			Information = 4,
			everything = 8
		}
		Logger logger;
		private void Awake() {
			logger = new Logger();
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

			switch (_LogLevel) {

				case LogLevel.Errors:
					LogErrors(condition,stacktrace,logtype);
					break;
				case LogLevel.Warnings:
					LogErrors(condition, stacktrace, logtype);
					LogWarnings(condition, stacktrace, logtype);
					break;
				case LogLevel.Information:
					LogErrors(condition, stacktrace, logtype);
					LogWarnings(condition, stacktrace, logtype);
					LogInformation(condition, stacktrace, logtype);
					break;
				case LogLevel.everything:
					LogErrors(condition, stacktrace, logtype);
					LogWarnings(condition, stacktrace, logtype);
					LogInformation(condition, stacktrace, logtype);
					LogEverything(condition, stacktrace, logtype);

					break;
				default:
					break;
			}

			//switch (logtype) {
			//	case LogType.Error:
			//		if (_LogLevel.HasFlag(LogLevel.Errors))
			//			Logger.WriteLogEntry(condition, logtype, stacktrace);
			//		break;
			//	case LogType.Assert:
			//		if (_LogLevel.HasFlag(LogLevel.everything))
			//			Logger.WriteLogEntry(condition, logtype);
			//		break;
			//	case LogType.Warning:
			//		if (_LogLevel.HasFlag(LogLevel.Warnings))
			//			Logger.WriteLogEntry(condition, logtype);
			//		break;
			//	case LogType.Log:
			//		if (_LogLevel.HasFlag(LogLevel.Information))
			//			Logger.WriteLogEntry(condition, logtype);
			//		break;
			//	case LogType.Exception:
			//		if (_LogLevel.HasFlag(LogLevel.Errors))
			//			Logger.WriteLogEntry(condition, logtype, stacktrace);
			//		break;
			//	default:
			//		break;
			//}
		}
		void LogErrors(string condition, string stacktrace, LogType logtype) {
			if (logtype == LogType.Error || logtype == LogType.Exception)
				Logger.WriteLogEntry(condition, logtype, stacktrace);
		}
		void LogWarnings(string condition, string stacktrace, LogType logtype) {
			if (logtype == LogType.Warning)
				Logger.WriteLogEntry(condition, logtype);
		}
		void LogInformation(string condition, string stacktrace, LogType logtype) {
			if (logtype == LogType.Log)
				Logger.WriteLogEntry(condition, logtype);
		}
		void LogEverything(string condition, string stacktrace, LogType logtype) {
			if (logtype == LogType.Assert)
				Logger.WriteLogEntry(condition, logtype);
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
		public Logger() {
			CurrLogFilePath = string.Concat(Application.persistentDataPath, "/_", DateTime.Now.ToString("yyMMdd"), "_Logfile", ".txt");
			CheckIfDirExists(CurrLogFilePath);
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
