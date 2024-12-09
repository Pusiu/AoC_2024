namespace AoC_2024
{
    public class Day9 : Day
    {
        public Day9() {isTest = false;}
        public override string Part1()
        {
            List<int> disk = new List<int>();
            int idx=0;
            for (int i=0; i < input.Length; i++)
            {
                bool isEmptySpace = i%2==1;
                for (int j=0; j < int.Parse(input[i].ToString());j++)
                {
                    disk.Add(isEmptySpace ? -1 : idx);
                }
                if (!isEmptySpace) idx++;
            }
            var arr = disk.ToArray();
            RemapDisk(arr);
            return CalculateChecksum(arr).ToString();
        }

        static long CalculateChecksum(int[] currentDisk)
        {
            long checkSum = 0;
            for (int i=0; i < currentDisk.Length; i++)
            {
                if (currentDisk[i] == -1) continue;
                checkSum += i*currentDisk[i];
            }
            return checkSum;
        }

        static void RemapDisk(int[] arr)
        {            
            int dataPointer = arr.Length-1;
            for (int i=0; i < arr.Length; i++)
            {
                if (dataPointer < i) return;
                if (arr[i] == -1)
                {
                    arr[i]=arr[dataPointer];
                    arr[dataPointer]=-1;
                    for (int j=dataPointer-1; j >=0 ;j--)
                    {
                        if (arr[j] != -1)
                        {
                            dataPointer=j;
                            break;
                        }
                    }
                }
            }
        }

        static void RemapDiskFiles(int[] arr)
        {
            int currentFileId = arr[^1];
            while (true) 
            {
                int dataPointer = arr.Length-1;
                if (currentFileId < 0) break;

                var blockEndIndex=-1;
                var blockStartIndex=0;
                for (int i=dataPointer; i > 0; i--)
                {
                    if (blockEndIndex == -1 && arr[i] == currentFileId)
                    {
                        blockEndIndex=i;
                    }
                    if (arr[i] != currentFileId && i < blockEndIndex)
                    {
                        blockStartIndex=i+1;
                        break;
                    }
                }
                if (blockStartIndex == 0) break;

                var freeSpaceStartIndex=-1;
                for (int i=0; i < arr.Length; i++)
                {
                    if (freeSpaceStartIndex > blockStartIndex) break;
                    if (arr[i] == -1)
                    {
                        if (freeSpaceStartIndex == -1)
                        {
                            freeSpaceStartIndex=i;
                        }
                    }
                    else
                    {
                        if (freeSpaceStartIndex != -1)
                        {
                            var length = i-freeSpaceStartIndex;
                            var fileLength=blockEndIndex-blockStartIndex+1;
                            if (length >= fileLength)
                            {
                                //file fits
                                for (int j=0; j < fileLength; j++)
                                {
                                    arr[freeSpaceStartIndex+j]=arr[blockStartIndex+j];
                                    arr[blockStartIndex+j]=-1;
                                }
                                break;
                            }
                            else
                            {
                                freeSpaceStartIndex=-1;
                            }
                        }
                    }
                }
                currentFileId--;
            }
        }

        public override string Part2()
        {
            List<int> disk = new List<int>();
            int idx=0;
            for (int i=0; i < input.Length; i++)
            {
                bool isEmptySpace = i%2==1;
                for (int j=0; j < int.Parse(input[i].ToString());j++)
                {
                    disk.Add(isEmptySpace ? -1 : idx);
                }
                if (!isEmptySpace) idx++;
            }
            var arr = disk.ToArray();
            RemapDiskFiles(arr);
            return CalculateChecksum(arr).ToString();
        }
    }
}