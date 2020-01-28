using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public enum Operation
    {
        Divide,
        Add,
        Substract,
        Multiply
    }
    public class Calculator
    {
        private const string delim = "-------"; 
        private Operation op;
        private double a;
        private double b;
        private int precision;
        public Operation Op { private get => op; set => op = value; }
        public double A { private get => a; set => a = value; }
        public double B
        {
            private get => b;
            set => b = value;
        }

        public int Precision 
        { 
            get => precision;
            set 
            {
                if (value > 0)
                {
                    precision = value;
                }
                else throw new ArgumentException("Precision cannot be less than 1!");
            }
        }

        //public Calculator() => Op = Operation.Add;
        public Calculator(double a, double b, Operation op = Operation.Add, int precision = 3)
        {
            A = a;
            B = b;
            Op = op;
            Precision = precision;
        }

        public void Calculate()
        {
            switch (Op)
            {
                case Operation.Divide:
                    Divide();
                    break;
                case Operation.Add:
                    Add();
                    break;
                case Operation.Substract:
                    Substract();
                    break;
                case Operation.Multiply:
                    Multiply();
                    break;
                default:
                    throw new InvalidOperationException("Invalid Operation!");
            }
        }

        private void Divide()
        {
            if (B == 0)
            {
                throw new DivideByZeroException("B cannot be zero!");
            }

            //FP is a fractional part
            //find len of a FP
            Regex part = new Regex("(,{1}.+)$");
            int lenFP1 = part.Match(a.ToString()).Length - 1;
            int lenFP2 = part.Match(b.ToString()).Length - 1;

            double mulCoeff = lenFP1 < 0 && lenFP2 < 0 ? 1 : Math.Pow(10, Math.Max(lenFP1, lenFP2));

            int divA = (int)(mulCoeff * a);
            int divB = (int)(mulCoeff * b);

            int intPart = 0;
            int sub = 0;
            string res = "";
            bool flag = false;
            int i = 0; //number of an operation
            //string delim = "-------";


            while (precision-- > 0)
            {
                Console.WriteLine($"\nOperation {++i}:");
                Console.WriteLine(delim);

                intPart = divA / divB;

                //output divA and minus sign
                Console.WriteLine("{0,5:0.#####}", divA);
                Console.WriteLine("-");

                res += intPart.ToString();
                sub = divA - divB * intPart;

                //output sub and tmp result
                Console.WriteLine("{0,5:0.#####}", divB * intPart);
                Console.Write("{0,5:0.#####}", sub);



                if (sub == 0)
                {
                    Console.WriteLine("\n" + delim);
                    break;
                }
                else if (sub == divA && flag && divA >= divB)
                {
                    break;
                }
                if (sub < divB)
                {
                    if (!flag)
                    {
                        res += ",";
                        flag = !flag;
                    }

                    divA = sub * 10;
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\nResult: " + res);
        }
        //private void Add() 
        //{
        //    int lenA = A.ToString().Replace(",", "").Length;
        //    int lenB = B.ToString().Replace(",", "").Length;

        //    int realMaxLen = Math.Max(lenA, lenB);
        //    int carry = 0;

        //    Console.WriteLine("{0,5:0.0000}", A);
        //    switch (Op)
        //    {
        //        case Operation.Add:
        //            Console.WriteLine("+");
        //            break;
        //        case Operation.Substract:
        //            Console.WriteLine("-");
        //            break;
                
        //        default:
        //            break;
        //    }
        //    //Console.WriteLine("+");
        //    Console.WriteLine("{0,5:0.0000}", B);
        //    Console.WriteLine(delim);

        //    //
        //    var pos = (Console.CursorLeft, Console.CursorTop);
    
        //    double aPart = A, bPart = B;
        //    //int power = 1;

        //    string _a = string.Format("{0:0000.####}", aPart);
        //    string _b = string.Format("{0:0000.####}", bPart);

        //    if (!_a.Contains(","))
        //    {
        //        _a += ",0";
        //    }            
        //    if (!_b.Contains(","))
        //    {
        //        _b += ",0";
        //    }

        //    int maxLen = Math.Max(_a.Length, _b.Length);
        //    int posNow = maxLen;

        //    Console.SetCursorPosition(pos.CursorLeft + (maxLen - realMaxLen + 1), pos.CursorTop);
        //    //realMaxLen++;

        //    while (maxLen-- > 0 && posNow-- >= 0)
        //    {
        //        if (A == 0 && B != 0)
        //        {
        //            Console.Write(B + "\b\b");
        //            break;
        //        }
        //        if (A != 0 && B == 0)
        //        {
        //            Console.Write(A + "\b\b");
        //            break;
        //        }
        //        var aCh = _a[posNow].ToString();
        //        var bCh = _b[posNow].ToString();
        //        if (aCh == "," && bCh == ",")
        //        {
        //            Console.Write(",\b\b");
        //            //posNow--;
        //            continue;
        //        }
        //        if (aCh == "0" && bCh == "0")
        //        {
        //            break;
        //        }


        //        aPart = double.Parse(aCh);
        //        bPart = double.Parse(bCh);
        //        //aPart /= Math.Floor(Math.Pow(10, power));
        //        //bPart /= Math.Floor(Math.Pow(10, power));
        //        double sum = aPart + bPart + carry;

        //        carry = 0;
        //        if (sum >= 10)
        //        {
        //            sum %= 10;
        //            carry = 1;
        //        }
        //        Console.Write(sum + "\b\b");
        //        //posNow--;
                
        //        //power++;
        //    }
        //}
        private void Add()
        {

            string strA = A.ToString();
            string strB = B.ToString();
            int posDelim = Math.Max(strA.IndexOf(","), strB.IndexOf(","));

            strA = strA.Replace(",", "");
            strB = strB.Replace(",", "");

            int cntZeros = 0;

            var nmbA = new int[Math.Abs(strA.Length-strB.Length)];
            var nmbB = new int[Math.Abs(strA.Length-strB.Length)];

            Console.WriteLine("{0,5:0.0000}", A);

            char symb = Op == Operation.Add? '+': '-';
            Console.WriteLine(symb);

            Console.WriteLine("{0,5:0.0000}", B);
            Console.WriteLine(delim);



            if (strA.Length == Math.Min(strA.Length, strB.Length))
            {
                cntZeros = strB.Length - strA.Length;
                var zeros = Enumerable.Repeat(0, cntZeros).ToArray();
                var nmbr = strA.ToArray().Select(s => int.Parse(s.ToString())).ToArray();
                nmbA = nmbr.Concat(zeros).ToArray();
                nmbB = strB.ToArray().Select(s => int.Parse(s.ToString())).ToArray();

            }
            else if (strB.Length == Math.Min(strA.Length, strB.Length))
            {
                cntZeros = strA.Length - strB.Length;
                var zeros = Enumerable.Repeat(0, cntZeros).ToArray();
                var nmbr = strB.ToArray().Select(s => int.Parse(s.ToString())).ToArray();
                nmbB = nmbr.Concat(zeros).ToArray();
                nmbA = strA.ToArray().Select(s => int.Parse(s.ToString())).ToArray();
            }

            int counter = nmbA.Length - 1;
            int carry = 0;

            var posCursor = (Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(posCursor.CursorLeft + nmbA.Length + 1, posCursor.CursorTop);

            while (true)
            {
                if (counter == 0 && posDelim != -1)
                {
                    Console.Write(",\b\b");
                    posDelim = -1;
                    continue;
                }

                if (counter < 0)
                    break;

                if (Op==Operation.Add)
                {
                    double sum = nmbA[counter] + nmbB[counter] + carry;

                    carry = 0;
                    if (sum >= 10)
                    {
                        sum %= 10;
                        carry = 1;
                    }
                    Console.Write(sum + "\b\b");
                }
                else if (Op == Operation.Substract)
                {
                    double sub = nmbA[counter] - nmbB[counter] - carry;
                    if (sub < 0)
                    {
                        sub += 10;
                        carry = 1;
                    }
                    else
                        carry = 0;

                    

                    Console.Write(sub + "\b\b");
                }

                counter--;
            }

            if (carry == 1)
                Console.Write(carry);
        }
        private void Substract() { Add(); }
        private void Multiply() { }

    }
}
