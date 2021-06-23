using System;

namespace TextHandler
{
    public static class InterfaceHandler
    {
        private const int STEP_PROGRESS_BAR = 5;
        private const int MAX_VALUE_PROGRESSBAR = 100;
        private const char FILL_BLOCK = '#';
        private const char EMPTY_BLOCK = '-';
        private static int currentPercent = default;
        public static string additionalMessageProgressBar = "Performing text analysis... ";

        public static void ResetProgressBar() => currentPercent = default;

        public static void RefreshProgressBar(long currentSize, long fullFileSize)
        {
            if (currentSize >= fullFileSize * currentPercent / 100)
            {
                WriteProgressBar(currentPercent + STEP_PROGRESS_BAR > MAX_VALUE_PROGRESSBAR ? MAX_VALUE_PROGRESSBAR : currentPercent);
                currentPercent += STEP_PROGRESS_BAR;
            }

            if (currentSize == fullFileSize && currentPercent <= MAX_VALUE_PROGRESSBAR)
            {
                RefreshProgressBar(currentSize, fullFileSize);
            }
        }

        public static void InteractionInterface()
        {
            throw new NotImplementedException();
        }

        private static void WriteProgressBar(int percent)
        {
            string message = additionalMessageProgressBar + "[";
            for (int i = 0; i < MAX_VALUE_PROGRESSBAR; i += 10)
            {
                message += i >= percent ? EMPTY_BLOCK : FILL_BLOCK;
            }

            message += string.Format("] {0,3:##0}% ", percent);
            Console.Write(new string('\b', message.Length) + message);
        }
    }
}
