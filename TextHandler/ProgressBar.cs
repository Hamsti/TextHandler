using System;
using System.Text;

namespace TextHandler
{
    public class ProgressBar
    {
        private const char FILL_BLOCK = '#';
        private const char EMPTY_BLOCK = '-';
        private const int MAX_VALUE_PROGRESSBAR = 100;
        private readonly int stepProgressBar;
        private readonly long fullFileSize;
        private readonly string additionalMessageProgressBar;
        private int currentPercent;
        private long countReadedBytesFile;

        /// <param name="fullFileSize">Full size of file</param>
        public ProgressBar(long fullFileSize)
        {
            this.fullFileSize = fullFileSize >= 0 ? fullFileSize : throw new ArgumentOutOfRangeException(nameof(fullFileSize));
        }

        /// <param name="stepProgressBar">1..99 step when progress bar update, or 100 for cancel display it</param>
        public ProgressBar(long fullFileSize, 
                           int stepProgressBar = 5, 
                           string additionalMessageProgressBar = "Performing text analysis... ") : this(fullFileSize)
        {
            this.stepProgressBar = stepProgressBar > 0 && stepProgressBar <= MAX_VALUE_PROGRESSBAR ? stepProgressBar :
                throw new ArgumentOutOfRangeException(nameof(stepProgressBar));
            this.additionalMessageProgressBar = additionalMessageProgressBar ?? 
                throw new ArgumentNullException(nameof(additionalMessageProgressBar));
        }

        /// <summary>
        /// Calculate and update the progress bar
        /// </summary>
        /// <param name="countBytes">The number of bytes to add to the size of the file read</param>
        public void Update(long countBytes)
        {
            if (countBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(countBytes));
            }
            else if (stepProgressBar == MAX_VALUE_PROGRESSBAR)
            {
                return;
            }
            else if ((countReadedBytesFile += countBytes) >= fullFileSize)
            {
                Write(MAX_VALUE_PROGRESSBAR);
            }
            else if (countReadedBytesFile >= fullFileSize * currentPercent / 100)
            {
                Write(currentPercent + stepProgressBar > MAX_VALUE_PROGRESSBAR ? MAX_VALUE_PROGRESSBAR : currentPercent);
                currentPercent += stepProgressBar;
            }
        }

        private void Write(int percent)
        {
            if (percent < 0 || percent > MAX_VALUE_PROGRESSBAR)
            {
                throw new ArgumentOutOfRangeException(nameof(percent));
            }

            StringBuilder message = new StringBuilder(additionalMessageProgressBar + '[');
            for (int i = 0; i < MAX_VALUE_PROGRESSBAR; i += 10)
            {
                message.Append(i >= percent ? EMPTY_BLOCK : FILL_BLOCK);
            }

            message.AppendFormat("] {0,3:##0}% ", percent);
            Console.Write(new string('\b', message.Length) + message);
        }
    }
}
