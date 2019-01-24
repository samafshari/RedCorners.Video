using System;
using System.Collections.Generic;
using System.Text;

namespace RedCorners
{
    [Serializable]
    public abstract class UploadTask
    {
        public abstract SupportedProviders Provider { get; }
        public virtual float GetProgress()
		{
			if (Feedback == null) return -1.0f;
			if (Feedback.LastByte >= Feedback.ContentSize) return 1.0f;
			return (float)Feedback.LastByte / Feedback.ContentSize;
		}
        public virtual bool IsFinished()
		{
			if (Done) return true;
			if (Feedback == null) return false;
			return Feedback.LastByte >= Feedback.ContentSize;
		}
        public bool Done = false;
        public string Path = null;
		public VerifyFeedback Feedback = null;
		public long LastByte = 0;
        public int ChunkSize = 10 * 1024 * 1024;
        public int MaxAttempts = 10;

        public override string ToString ()
		{
			return "Done: " + Done + "\n" +
			"Path: " + (Path ?? "null") + "\n" +
			"LastByte: " + LastByte + "\n" +
			"Progress: " + GetProgress () + "\n";
		}
    }
}
