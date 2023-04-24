using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading;
using System.Globalization;



namespace TGP.Utilities {
	public class LoggerManager : BaseMonoBehaviour {
		Logger logger;
		private void Awake() {
			logger = new Logger();
			Logger.CreateAppLogFile();
			if (debug)
				logger.debug = debug;
			}
		private void OnEnable() {
			Application.logMessageReceived += LogCallbacksToFile;
			}
		private void OnDisable() {
			Application.logMessageReceived -= LogCallbacksToFile;
			}
		//todo: remove
		//C:\Users\mail\AppData\LocalLow\TheGoodplace\meatlesscooking

		void LogCallbacksToFile(string condition, string stacktrace, LogType logtype) {
			switch (logtype) {
				case LogType.Error:
					Logger.WriteLogEntry(condition, logtype, stacktrace);
					break;
				case LogType.Assert:
					break;
				case LogType.Warning:
					Logger.WriteLogEntry(condition, logtype);
					break;
				case LogType.Log:
					Logger.WriteLogEntry(condition, logtype);
					break;
				case LogType.Exception:
					Logger.WriteLogEntry(condition, logtype, stacktrace);
					break;
				default:
					break;
				}
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
				}
			catch (Exception ex) {
				Debug.LogWarningFormat("error whilewriting Log   Message:{0}", ex);
				}
			finally {
				lock_.ExitWriteLock();
				}
			}
		}

	}
