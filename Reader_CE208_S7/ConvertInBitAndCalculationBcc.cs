using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInBitAndCalculationBcc
{

    public class ParityCalculation
    {
        protected int[] ConvertFromByteInBit(byte b) // Преобразование byte в масив типа bit.
        {
            int[] Bit = new int[8];
            for (int i = 0; i < Bit.Length; i++)
            {
                int bit = 0;
                bit = (b >> i) & 0x01;
                Bit[i] = bit;
            }

            return Bit;
        }

        private int[] ParityScore(byte sorceByte) // Подсчёт программной чётности.
        {
            int[] bitConvert = ConvertFromByteInBit(sorceByte);
            int parity = 0;
            for (int i = 0; i < bitConvert.Length; i++)
            {
                parity += bitConvert[i];
            }
            if (parity % 2 != 0)
            {
                bitConvert[7] = 1;
            }
            return bitConvert;
        }

        /// <summary>
        /// Сводка:
        /// Преобразует значение масива типа byte в масив типа bit, считает программную четность, добовляет программную четность по необходимости и преобразует полученнный масив типа bit
        /// в масив типа byte.
        ///
        /// Параметры:
        /// input - принимает на вход масив типа byte.
        /// 
        /// Возврат:
        /// Масив типа byte, после подсчёта чётности.
        /// </summary>
        public byte[] ResultScore(byte[] input)
        {
            byte[] outByte = new byte[input.Length];
            for (int a = 0; a < input.Length; a++)
            {
                int[] convertedBit = ParityScore(input[a]);
                string binaryСode = "";
                for (int i = convertedBit.Length - 1; i >= 0; i--)
                {
                    binaryСode += Convert.ToString(convertedBit[i]);
                }
                outByte[a] = Convert.ToByte(binaryСode, 2);
            }
            return outByte;
        }
    }

    public static class CalculationBcc
    {

        /// <summary>
        /// Сводка:
        /// Считает контрольный код BCC, методом арифметического суммирования.
        ///
        /// Параметры:
        /// input - принимает на вход масив типа byte.
        /// 
        /// Возврат:
        /// Новый масив типа byte, с добавленным кодом BCC.
        /// </summary>
        public static byte[] calculationBcc(byte[] input)
        {
            int initialByte = Array.IndexOf<byte>(input, 0x01);
            initialByte++;
            byte[] outByte = new byte[input.Length + 1];
            byte Bcc = 0;
            for (; initialByte < input.Length; initialByte++)
            {
                Bcc += input[initialByte];
            }
            Bcc &= 0x7F;
            input.CopyTo(outByte, 0);
            outByte[outByte.Length - 1] = Bcc;
            return outByte;
        }
    }

    public class DiscardingParity : RequestConverter
    {
        private int[] ParityScore(byte sorceByte) // Удаление программной чётности
        {
            int[] bitConvert = ConvertFromByteInBit(sorceByte);
            int parity = 0;
            for (int i = 0; i < bitConvert.Length; i++)
            {
                parity += bitConvert[i];
            }
            if (parity % 2 == 0)
            {
                bitConvert[7] = 0;
            }
            return bitConvert;
        }

        /// <summary>
        /// Сводка:
        /// Преобразует значение масива типа byte в масив типа bit, считает программную четность, удаляет программную четность по необходимости и преобразует полученнный масив типа bit
        /// в масив типа byte.
        /// 
        /// Параметры:
        /// input - принимает на вход масив типа byte.
        /// 
        /// Возврат:
        /// Новый масив типа byte после удаления программной четности.
        /// </summary>
        public byte[] DiscardingParityScore(byte[] input)
        {
            byte[] outByte = new byte[input.Length];
            for (int a = 0; a < input.Length; a++)
            {
                int[] convertedBit = ParityScore(input[a]);
                string binaryСode = "";
                for (int i = convertedBit.Length - 1; i >= 0; i--)
                {
                    binaryСode += Convert.ToString(convertedBit[i]);
                }
                outByte[a] = Convert.ToByte(binaryСode, 2);
            }
            return outByte;
        }
    }

    public class RequestConverter : ParityCalculation
    {
        /// <summary>
        /// Сводка:
        /// Получает на вход строку запроса, ищет в строке подстроку, поиск идет по всей строке, преобразует найденную подстроку в byte и добовляет преобразованный byte в коллекцию, 
        /// после прохода по всей строке, определяет необходимость подсчёта контрольного кода BCC, преобразует коллекцию в масив типа byte, считает BCC по необходимости и отправляет полученный 
        /// масив byte в метод ResultScore класса ParityCalculation.
        /// 
        /// Параметры:
        /// request - принимает на вход запрос типа string.
        /// 
        /// Возврат:
        /// Новый масив типа byte после прохода через метод ResultScore класса ParityCalculation.
        /// </summary>
        public byte[] Converter(string request)
       {
            int i = 0;
            
            List<byte> result = new List<byte>();
            if(request.IndexOf("/?!") != -1)
            {
                for (; i < 3; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }

            if (request.IndexOf("<SOH>") != -1)
            {
                result.Add(0x01);
                i += 5;
            }

            if (request.IndexOf("<ACK>") != -1)
            {
                result.Add(0x06);
                i += 5;
            }

            if (request.IndexOf("R1") != -1)
            {
                for (int j = i; i < j + 2; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }

            if(request.IndexOf("P1") != -1)
            {
                for(int j = i; i < j + 2; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }

            if (request.IndexOf("051") != -1)
            {
                for (int j = i; i < j + 3; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }

            if (request.IndexOf("<STX>") != -1)
            {
                result.Add(0x02);
                i += 5;
            }
            
            
            if (request.IndexOf("(777777)") != -1)
            {
                for (int j = i; i < j + 8; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }
            if (request.IndexOf("VOLTA()") != -1)
            {
                for (int j = i; i < j + 7; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }
            if (request.IndexOf("FREQU()") != -1)
            {
                for (int j = i; i < j + 7; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }
            if (request.IndexOf("WATCH()") != -1)
            {
                for (int j = i; i < j + 7; i++)
                {
                    result.Add(Convert.ToByte(request[i]));
                }
            }
            if (request.IndexOf("<CR>") != -1)
            {
                result.Add(0x0D);
                i += 4;
            }
            if (request.IndexOf("<LF>") != -1)
            {
                result.Add(0x0A);
                i += 4;
            }
            if (request.IndexOf("<ETX>") != -1)
            {
                result.Add(0x03);
                i += 4;
            }
            int g = 0;
            if (request.IndexOf("<BCC>") != -1)
            {
                g++;
            }
            byte[] input = new byte[result.Count + g]; 
            if (request.IndexOf("<BCC>") != -1) // Определяет необходимость подсчёта BCC.
            {
                input = CalculationBcc.calculationBcc(result.GetRange(0, result.Count).ToArray());
            }
            else
            {
                input = result.GetRange(0, result.Count).ToArray();
            }
            
            return ResultScore(input);
       }
    }
}
