using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace EditorUtility
{
    public static class EditorTools
    {
        public enum Position { left, right }
        public enum Alignment { left, center, right }

        public static Rect SetInspectorXPosition(Position XPosition, float y, float width, float height)
        {
            float x = 18f;
            if(XPosition == Position.right)
            {
                float indentValue = EditorGUI.indentLevel * 15f;
                x += EditorGUIUtility.labelWidth - indentValue;
            }
            return new Rect(x, y, width, height);
        }
        
    }
    /// <summary>
    /// An error log saved in a dictionary. Compares with a condition before adding an error message to a dictionary.
    /// If the condition is false it will instead remove the message from the collection.
    /// </summary>
    public class ErrorLog
    {
        private Dictionary<string, MessageType> errorLog = new Dictionary<string, MessageType>();

        /// <returns>
        /// List of strings of error messages.
        /// </returns>
        public List<string> GetError => errorLog.Select(x => x.Key).ToList();

        /// <returns>
        /// Dictionary of the whole log.
        /// </returns>
        public Dictionary<string, MessageType> GetFull => errorLog;

        /// <summary>
        /// Adds an error message into the log.
        /// </summary>
        /// <param name="condition">The condition to compare with. Adds to the log if true, removes from the log if false</param>
        /// <param name="message">The message to be added to the log.</param>
        /// <param name="type">MessageType of the Help Box to be drawn.</param>
        /// <param name="doIfError">Optional delegate to run if the condition is true.</param>
        /// <param name="doElse">Optional delegate to run if the condition is false.</param>
        public bool Add(bool condition, string message, MessageType type, Action doIfError = null, Action doElse = null) =>
            ErrorMessage(condition, ref errorLog, message, type, doIfError, doElse);

        /// <summary>
        /// Draws all errors to a help box.
        /// </summary>
        /// <returns>Returns false if there are no errors, otherwise true.</returns>
        /// <param name="messageTypes">If messageTypes are defined, will only include errors with that MessageType</param>
        /// <returns></returns>
        public bool DrawErrors(params MessageType[] messageTypes)
        {
            if(errorLog.Count == 0) return false;

            var output = !messageTypes.Any() ? errorLog : errorLog.Where(x => messageTypes.Contains(x.Value));
            foreach(var valuePair in output)
            {
                EditorGUILayout.HelpBox(valuePair.Key, valuePair.Value);
                EditorGUILayout.Space();
            }
            return true;
        }
        public void SortElements(params MessageType[] sortOrder)
        {
            errorLog = errorLog.OrderBy(x => Array.IndexOf(sortOrder, x.Value)).ToDictionary(x => x.Key, x => x.Value);
        }

        ///<summary>Returns number of errors, specified by parameters. Will count all errors if no parameters are entered.</summary>
        ///<returns>Returns int of number of errors.</returns>
        public int GetErrors(params MessageType[] messageTypes) =>
            !messageTypes.Any() ? errorLog.Count : errorLog.Count(x => messageTypes.Contains(x.Value));

        private static bool ErrorMessage(bool condition, ref Dictionary<string, MessageType> dict, string message, MessageType type, Action doIfError = null, Action doElse = null)
        {
            if(condition)
            {
                if(!dict.ContainsKey(message))
                    dict.Add(message, type);
                doIfError?.Invoke();
            }
            else
            {
                dict.Remove(message);
                doElse?.Invoke();
            }
            return condition;
        }
    }
}
