using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ObjectOrientedCalculator;

public class Operation
{
    private char[] totalOperands = { '^', '*', '/', '+', '-' };
   
    public string CalcSolve(string input)
    {
        input = input.Trim();
        input = input.Replace(" ", "");

        if ((totalOperands.Contains(input[0]) && input[0] != '-') || totalOperands.Contains(input[input.Length - 1]))
        {
            return "Bad Operand Placement";
        }

        List<string> args = new();

        bool doubleOperandCheck = false;
        bool isNeg = (input[0] == '-');
        string inputNum = "";
        foreach(char val in input)
        {
            if(totalOperands.Contains(val) && !doubleOperandCheck && !isNeg)
            {
                args.Add(inputNum);
                args.Add(val + "");
                inputNum = "";
                doubleOperandCheck = true;
            }
            else if(totalOperands.Contains(val) && doubleOperandCheck && val != '-')
            {
                return "Double Operands Please Remove";
            }
            else
            {
                inputNum += val;
                doubleOperandCheck = false;
            }
            isNeg = false;
        }
        args.Add(inputNum);

        int i = 0;
        foreach(char operand in totalOperands)
        {
            for(i = 1; i < args.Count() - 1; i++)
            {
                if (args[i][0] == operand)
                {
                    
                    double temp = doMath(double.Parse(args[i - 1]), args[i][0], double.Parse(args[i + 1]));

                    args.Insert(i - 1, temp + "");
                    args.RemoveRange(i, 3);
                }
            }
        }

        return args[0];
    }

    private double doMath(double x, char operand, double y)
    {
        switch(operand)
        {
            case '^':
                return Math.Pow(x, y);
            case '*':
                return x * y;
            case '/':
                return x / y;
            case '+':
                return x + y;
            case '-':
                return x - y;
            default:
                break;
        }

        throw new Exception($"Math Falure {x}, {operand}, {y}");
    }
}
