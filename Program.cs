using RpnCalculator;

var exp1 = "6825+8247*";
var res = exp1.ConvertToExpression();
Console.WriteLine(res);
var compile = res.Compile();
Console.WriteLine(compile());