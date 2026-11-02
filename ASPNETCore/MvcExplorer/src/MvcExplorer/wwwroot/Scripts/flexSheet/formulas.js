c1.documentReady(function () {
    var flexSheet = wijmo.Control.getControl('#formulaSheet');
    for (var i = 0; i < flexSheet.sheets.length; i++) {
        flexSheet.sheets.selectedIndex = i;
        switch (flexSheet.sheets[i].name) {
            case 'Basic Operators':
                generateBasicOperatorsSheet(flexSheet);
                break;
            case 'Math':
                generateMathSheet(flexSheet);
                break;
            case 'Logical':
                generateLogicalSheet(flexSheet);
                break;
            case 'Text':
                generateTextSheet(flexSheet);
                break;
            case 'Aggregate':
                generateAggregateSheet(flexSheet);
                break;
            case 'Date':
                generateDateSheet(flexSheet);
                break;
            case 'Lookup & Reference':
                generateLookupReferenceSheet(flexSheet);
                break;
            case 'Financial':
                generateFinancialSheet(flexSheet);
                break;
            case 'Summary':
                generateSummraySheet(flexSheet);
                break;
        }
    }
    flexSheet.selectedSheetIndex = 0;

});

function generateBasicOperatorsSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Positive/Negative Numbers");
    flexSheet.setCellData(1, 1, "Input a Positive/Negative number.");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "-1");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=-1");
    flexSheet.setCellData(4, 1, "2. Add/Subtract Operators");
    flexSheet.setCellData(5, 1, "Calculates add/sub expression.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "1.25 + 2.17");
    flexSheet.setCellData(6, 3, "Result:");
    flexSheet.setCellData(6, 4, "=1.25 + 2.17");
    flexSheet.setCellData(7, 1, "Sample:");
    flexSheet.setCellData(7, 2, "2.23 - 3.51");
    flexSheet.setCellData(7, 3, "Result:");
    flexSheet.setCellData(7, 4, "=2.23 - 3.51");
    flexSheet.setCellData(9, 1, "3. Multiplication/Division Operators");
    flexSheet.setCellData(10, 1, "Calculates mul/div expression.");
    flexSheet.setCellData(11, 1, "Sample:");
    flexSheet.setCellData(11, 2, "12 * 17");
    flexSheet.setCellData(11, 3, "Result:");
    flexSheet.setCellData(11, 4, "=12 * 17");
    flexSheet.setCellData(12, 1, "Sample:");
    flexSheet.setCellData(12, 2, "20 / 6");
    flexSheet.setCellData(12, 3, "Result:");
    flexSheet.setCellData(12, 4, "=20 / 6");
    flexSheet.setCellData(14, 1, "4. Power Operator");
    flexSheet.setCellData(15, 1, "Calculates power expression.");
    flexSheet.setCellData(16, 1, "Sample:");
    flexSheet.setCellData(16, 2, "2^3");
    flexSheet.setCellData(16, 3, "Result:");
    flexSheet.setCellData(16, 4, "=2^3");
    flexSheet.setCellData(18, 1, "5. Bracket");
    flexSheet.setCellData(19, 1, "Indicates calculation priority by the bracket.");
    flexSheet.setCellData(20, 1, "Sample:");
    flexSheet.setCellData(20, 2, "((1+2)*3)/((4-2)*2)");
    flexSheet.setCellData(21, 1, "Result:");
    flexSheet.setCellData(21, 2, "=((1+2)*3)/((4-2)*2)");
    flexSheet.setCellData(23, 1, "6. Percentage");
    flexSheet.setCellData(24, 1, "Parse the percentage to float number.");
    flexSheet.setCellData(25, 1, "Sample:");
    flexSheet.setCellData(25, 2, "23%");
    flexSheet.setCellData(25, 3, "Result:");
    flexSheet.setCellData(25, 4, "=23%");
    flexSheet.setCellData(27, 1, "7. Scientific Number");
    flexSheet.setCellData(28, 1, "Parse the scientific number to float number.");
    flexSheet.setCellData(29, 1, "Sample:");
    flexSheet.setCellData(29, 2, "1.2556e2");
    flexSheet.setCellData(29, 3, "Result:");
    flexSheet.setCellData(29, 4, "=1.2556e2");
    flexSheet.setCellData(31, 1, "8. Text Concatenate");
    flexSheet.setCellData(32, 1, "Joins two text items into one text item.");
    flexSheet.setCellData(33, 1, "Sample:");
    flexSheet.setCellData(33, 2, "\"Hello \" & \"World\"");
    flexSheet.setCellData(33, 3, "Result:");
    flexSheet.setCellData(33, 4, "=\"Hello \" & \"World\"");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 2),
        new wijmo.grid.CellRange(4, 1, 4, 2),
        new wijmo.grid.CellRange(9, 1, 9, 2),
        new wijmo.grid.CellRange(14, 1, 14, 2),
        new wijmo.grid.CellRange(18, 1, 18, 2),
        new wijmo.grid.CellRange(23, 1, 23, 2),
        new wijmo.grid.CellRange(27, 1, 27, 2),
        new wijmo.grid.CellRange(31, 1, 31, 2)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 7, 1), new wijmo.grid.CellRange(6, 3, 7, 3),
        new wijmo.grid.CellRange(11, 1, 12, 1), new wijmo.grid.CellRange(11, 3, 12, 3),
        new wijmo.grid.CellRange(16, 1, 16, 1), new wijmo.grid.CellRange(16, 3, 16, 3),
        new wijmo.grid.CellRange(20, 1, 21, 1),
        new wijmo.grid.CellRange(25, 1, 25, 1), new wijmo.grid.CellRange(25, 3, 25, 3),
        new wijmo.grid.CellRange(29, 1, 29, 1), new wijmo.grid.CellRange(29, 3, 29, 3),
        new wijmo.grid.CellRange(33, 1, 33, 1), new wijmo.grid.CellRange(33, 3, 33, 3)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 1, 0, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(4, 1, 4, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 1, 9, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(10, 1, 10, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(14, 1, 14, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(15, 1, 15, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(19, 1, 19, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(20, 2, 20, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(23, 1, 23, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(24, 1, 24, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(27, 1, 27, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(28, 1, 28, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(31, 1, 31, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(32, 1, 32, 3));
}

function generateMathSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Pi");
    flexSheet.setCellData(1, 1, "Returns the value of pi.");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "pi()");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=pi()");
    flexSheet.setCellData(4, 1, "2. Rand");
    flexSheet.setCellData(5, 1, "Returns a random number between 0 and 1.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "rand()");
    flexSheet.setCellData(6, 3, "Result:");
    flexSheet.setCellData(6, 4, "=rand()");
    flexSheet.setCellData(8, 1, "3. Abs");
    flexSheet.setCellData(9, 1, "Returns the absolute value of a number.");
    flexSheet.setCellData(10, 1, "Sample:");
    flexSheet.setCellData(10, 2, "abs(-2.73)");
    flexSheet.setCellData(10, 3, "Result:");
    flexSheet.setCellData(10, 4, "=abs(-2.73)");
    flexSheet.setCellData(12, 1, "4. Acos");
    flexSheet.setCellData(13, 1, "Returns the arccosine of a number.");
    flexSheet.setCellData(14, 1, "Sample:");
    flexSheet.setCellData(14, 2, "acos(0.35)");
    flexSheet.setCellData(14, 3, "Result:");
    flexSheet.setCellData(14, 4, "=acos(0.35)");
    flexSheet.setCellData(16, 1, "5. Asin");
    flexSheet.setCellData(17, 1, "Returns the arcsine of a number.");
    flexSheet.setCellData(18, 1, "Sample:");
    flexSheet.setCellData(18, 2, "asin(0.5)");
    flexSheet.setCellData(18, 3, "Result:");
    flexSheet.setCellData(18, 4, "=asin(0.5)");
    flexSheet.setCellData(20, 1, "6. Atan");
    flexSheet.setCellData(21, 1, "Returns the arctangent of a number.");
    flexSheet.setCellData(22, 1, "Sample:");
    flexSheet.setCellData(22, 2, "atan(0.67)");
    flexSheet.setCellData(22, 3, "Result:");
    flexSheet.setCellData(22, 4, "=atan(0.67)");
    flexSheet.setCellData(24, 1, "7. Cos");
    flexSheet.setCellData(25, 1, "Returns the cosine of a number.");
    flexSheet.setCellData(26, 1, "Sample:");
    flexSheet.setCellData(26, 2, "cos(0.6)");
    flexSheet.setCellData(26, 3, "Result:");
    flexSheet.setCellData(26, 4, "=cos(0.6)");
    flexSheet.setCellData(28, 1, "8. Sin");
    flexSheet.setCellData(29, 1, "Returns the sine of the given angle.");
    flexSheet.setCellData(30, 1, "Sample:");
    flexSheet.setCellData(30, 2, "sin(0.5)");
    flexSheet.setCellData(30, 3, "Result:");
    flexSheet.setCellData(30, 4, "=sin(0.5)");
    flexSheet.setCellData(32, 1, "9. Tan");
    flexSheet.setCellData(33, 1, "Returns the tangent of a number.");
    flexSheet.setCellData(34, 1, "Sample:");
    flexSheet.setCellData(34, 2, "tan(0.75)");
    flexSheet.setCellData(34, 3, "Result:");
    flexSheet.setCellData(34, 4, "=tan(0.75)");
    flexSheet.setCellData(36, 1, "10. Atan2");
    flexSheet.setCellData(37, 1, "Returns the arctangent from x- and y-coordinates.");
    flexSheet.setCellData(38, 1, "Sample:");
    flexSheet.setCellData(38, 2, "atan2(90, 15)");
    flexSheet.setCellData(38, 3, "Result:");
    flexSheet.setCellData(38, 4, "=atan2(90, 15)");
    flexSheet.setCellData(40, 1, "11. Ceiling");
    flexSheet.setCellData(41, 1, "Rounds a number to the nearest integer or to the nearest multiple of significance.");
    flexSheet.setCellData(42, 1, "Sample:");
    flexSheet.setCellData(42, 2, "ceiling(6.03)");
    flexSheet.setCellData(42, 3, "Result:");
    flexSheet.setCellData(42, 4, "=ceiling(6.03)");
    flexSheet.setCellData(44, 1, "12. Floor");
    flexSheet.setCellData(45, 1, "Rounds a number down, toward zero.");
    flexSheet.setCellData(46, 1, "Sample:");
    flexSheet.setCellData(46, 2, "floor(7.96)");
    flexSheet.setCellData(46, 3, "Result:");
    flexSheet.setCellData(46, 4, "=floor(7.96)");
    flexSheet.setCellData(48, 1, "13. Round");
    flexSheet.setCellData(49, 1, "Rounds a number to a specified number of digits.");
    flexSheet.setCellData(50, 1, "Sample:");
    flexSheet.setCellData(50, 2, "round(7.56, 1)");
    flexSheet.setCellData(50, 3, "Result:");
    flexSheet.setCellData(50, 4, "=round(7.56, 1)");
    flexSheet.setCellData(51, 1, "Sample:");
    flexSheet.setCellData(51, 2, "round(7.54, 1)");
    flexSheet.setCellData(51, 3, "Result:");
    flexSheet.setCellData(51, 4, "=round(7.54, 1)");
    flexSheet.setCellData(53, 1, "14. Exp");
    flexSheet.setCellData(54, 1, "Returns e raised to the power of a given number.");
    flexSheet.setCellData(55, 1, "Sample:");
    flexSheet.setCellData(55, 2, "exp(-1)");
    flexSheet.setCellData(55, 3, "Result:");
    flexSheet.setCellData(55, 4, "=exp(-1)");
    flexSheet.setCellData(57, 1, "15. Ln");
    flexSheet.setCellData(58, 1, "Returns the natural logarithm of a number.");
    flexSheet.setCellData(59, 1, "Sample:");
    flexSheet.setCellData(59, 2, "ln(15)");
    flexSheet.setCellData(59, 3, "Result:");
    flexSheet.setCellData(59, 4, "=ln(15)");
    flexSheet.setCellData(61, 1, "16. Sqrt");
    flexSheet.setCellData(62, 1, "Returns a positive square root.");
    flexSheet.setCellData(63, 1, "Sample:");
    flexSheet.setCellData(63, 2, "sqrt(16)");
    flexSheet.setCellData(63, 3, "Result:");
    flexSheet.setCellData(63, 4, "=sqrt(16)");
    flexSheet.setCellData(65, 1, "17. Power");
    flexSheet.setCellData(66, 1, "Returns the result of a number raised to a power.");
    flexSheet.setCellData(67, 1, "Sample:");
    flexSheet.setCellData(67, 2, "power(1.5, 0.5)");
    flexSheet.setCellData(67, 3, "Result:");
    flexSheet.setCellData(67, 4, "=power(1.5, 0.5)");
    flexSheet.setCellData(69, 1, "18. Mod");
    flexSheet.setCellData(70, 1, "Returns the remainder from division.");
    flexSheet.setCellData(71, 1, "Sample:");
    flexSheet.setCellData(71, 2, "mod(11, 3)");
    flexSheet.setCellData(71, 3, "Result:");
    flexSheet.setCellData(71, 4, "=mod(11, 3)");
    flexSheet.setCellData(73, 1, "19. Rounddown");
    flexSheet.setCellData(74, 1, "Rounds a number down, toward zero.");
    flexSheet.setCellData(75, 1, "Sample:");
    flexSheet.setCellData(75, 2, "rounddown(11.987, 2)");
    flexSheet.setCellData(76, 1, "Result:");
    flexSheet.setCellData(76, 2, "=rounddown(11.987, 2)");
    flexSheet.setCellData(78, 1, "20. Roundup");
    flexSheet.setCellData(79, 1, "Rounds a number up, away from zero.");
    flexSheet.setCellData(80, 1, "Sample:");
    flexSheet.setCellData(80, 2, "roundup(11.982, 2)");
    flexSheet.setCellData(81, 1, "Result:");
    flexSheet.setCellData(81, 2, "=roundup(11.982, 2)");
    flexSheet.setCellData(83, 1, "21. Trunc");
    flexSheet.setCellData(84, 1, "Truncates a number to an integer.");
    flexSheet.setCellData(85, 1, "Sample:");
    flexSheet.setCellData(85, 2, "trunc(8.9)");
    flexSheet.setCellData(85, 3, "Result:");
    flexSheet.setCellData(85, 4, "=trunc(8.9)");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 4, 1),
        new wijmo.grid.CellRange(8, 1, 8, 1),
        new wijmo.grid.CellRange(12, 1, 12, 1),
        new wijmo.grid.CellRange(16, 1, 16, 1),
        new wijmo.grid.CellRange(20, 1, 20, 1),
        new wijmo.grid.CellRange(24, 1, 24, 1),
        new wijmo.grid.CellRange(28, 1, 28, 1),
        new wijmo.grid.CellRange(32, 1, 32, 1),
        new wijmo.grid.CellRange(36, 1, 36, 1),
        new wijmo.grid.CellRange(40, 1, 40, 1),
        new wijmo.grid.CellRange(44, 1, 44, 1),
        new wijmo.grid.CellRange(48, 1, 48, 1),
        new wijmo.grid.CellRange(53, 1, 53, 1),
        new wijmo.grid.CellRange(57, 1, 57, 1),
        new wijmo.grid.CellRange(61, 1, 61, 1),
        new wijmo.grid.CellRange(65, 1, 65, 1),
        new wijmo.grid.CellRange(69, 1, 69, 1),
        new wijmo.grid.CellRange(73, 1, 73, 1),
        new wijmo.grid.CellRange(78, 1, 78, 1),
        new wijmo.grid.CellRange(83, 1, 83, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 6, 1), new wijmo.grid.CellRange(6, 3, 6, 3),
        new wijmo.grid.CellRange(10, 1, 10, 1), new wijmo.grid.CellRange(10, 3, 10, 3),
        new wijmo.grid.CellRange(14, 1, 14, 1), new wijmo.grid.CellRange(14, 3, 14, 3),
        new wijmo.grid.CellRange(18, 1, 18, 1), new wijmo.grid.CellRange(18, 3, 18, 3),
        new wijmo.grid.CellRange(22, 1, 22, 1), new wijmo.grid.CellRange(22, 3, 22, 3),
        new wijmo.grid.CellRange(26, 1, 26, 1), new wijmo.grid.CellRange(26, 3, 26, 3),
        new wijmo.grid.CellRange(30, 1, 30, 1), new wijmo.grid.CellRange(30, 3, 30, 3),
        new wijmo.grid.CellRange(34, 1, 34, 1), new wijmo.grid.CellRange(34, 3, 34, 3),
        new wijmo.grid.CellRange(38, 1, 38, 1), new wijmo.grid.CellRange(38, 3, 38, 3),
        new wijmo.grid.CellRange(42, 1, 42, 1), new wijmo.grid.CellRange(42, 3, 42, 3),
        new wijmo.grid.CellRange(46, 1, 47, 1), new wijmo.grid.CellRange(46, 3, 47, 3),
        new wijmo.grid.CellRange(50, 1, 51, 1), new wijmo.grid.CellRange(50, 3, 51, 3),
        new wijmo.grid.CellRange(55, 1, 55, 1), new wijmo.grid.CellRange(55, 3, 55, 3),
        new wijmo.grid.CellRange(59, 1, 59, 1), new wijmo.grid.CellRange(59, 3, 59, 3),
        new wijmo.grid.CellRange(63, 1, 63, 1), new wijmo.grid.CellRange(63, 3, 63, 3),
        new wijmo.grid.CellRange(67, 1, 67, 1), new wijmo.grid.CellRange(67, 3, 67, 3),
        new wijmo.grid.CellRange(71, 1, 71, 1), new wijmo.grid.CellRange(71, 3, 71, 3),
        new wijmo.grid.CellRange(75, 1, 76, 1),
        new wijmo.grid.CellRange(80, 1, 81, 1),
        new wijmo.grid.CellRange(85, 1, 85, 1), new wijmo.grid.CellRange(85, 3, 85, 3)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 1, 9, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(13, 1, 13, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(17, 1, 17, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(21, 1, 21, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(25, 1, 25, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(29, 1, 29, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(33, 1, 33, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(37, 1, 37, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(41, 1, 41, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(45, 1, 45, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(49, 1, 49, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(54, 1, 54, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(58, 1, 58, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(62, 1, 62, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(66, 1, 66, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(70, 1, 70, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(73, 1, 73, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(74, 1, 74, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(75, 2, 75, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(79, 1, 79, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(80, 2, 80, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(84, 1, 84, 2));
}

function generateLogicalSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Compare operators");
    flexSheet.setCellData(1, 1, "Gets boolean result of the compare operators such as (>, <, >=, <=, =, <>).");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "1>2");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=1>2");
    flexSheet.setCellData(4, 1, "2. True");
    flexSheet.setCellData(5, 1, "Returns the logical value TRUE.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "true()");
    flexSheet.setCellData(6, 3, "Result:");
    flexSheet.setCellData(6, 4, "=true()");
    flexSheet.setCellData(8, 1, "3. False");
    flexSheet.setCellData(9, 1, "Returns the logical value FALSE.");
    flexSheet.setCellData(10, 1, "Sample:");
    flexSheet.setCellData(10, 2, "false()");
    flexSheet.setCellData(10, 3, "Result:");
    flexSheet.setCellData(10, 4, "=false()");
    flexSheet.setCellData(12, 1, "4. And");
    flexSheet.setCellData(13, 1, "Returns TRUE if all of its arguments are TRUE.");
    flexSheet.setCellData(14, 1, "Sample:");
    flexSheet.setCellData(14, 2, "and(true(),1>2)");
    flexSheet.setCellData(14, 3, "Result:");
    flexSheet.setCellData(14, 4, "=and(true(),1>2)");
    flexSheet.setCellData(16, 1, "5. Or");
    flexSheet.setCellData(17, 1, "Returns TRUE if any argument is TRUE.");
    flexSheet.setCellData(18, 1, "Sample:");
    flexSheet.setCellData(18, 2, "or(false(),1<2)");
    flexSheet.setCellData(18, 3, "Result:");
    flexSheet.setCellData(18, 4, "=or(false(),1<2)");
    flexSheet.setCellData(20, 1, "6. Not");
    flexSheet.setCellData(21, 1, "Reverses the logic of its argument.");
    flexSheet.setCellData(22, 1, "Sample:");
    flexSheet.setCellData(22, 2, "not(1<2)");
    flexSheet.setCellData(22, 3, "Result:");
    flexSheet.setCellData(22, 4, "=not(1<2)");
    flexSheet.setCellData(24, 1, "7. If");
    flexSheet.setCellData(25, 1, "Specifies a logical test to perform.");
    flexSheet.setCellData(26, 1, "Sample:");
    flexSheet.setCellData(26, 2, "if(true(), \"true result\", \"false result\")");
    flexSheet.setCellData(27, 1, "Result:");
    flexSheet.setCellData(27, 2, "=if(true(), \"true result\", \"false result\")");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 4, 1),
        new wijmo.grid.CellRange(8, 1, 8, 1),
        new wijmo.grid.CellRange(12, 1, 12, 1),
        new wijmo.grid.CellRange(16, 1, 16, 1),
        new wijmo.grid.CellRange(20, 1, 20, 1),
        new wijmo.grid.CellRange(24, 1, 24, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 6, 1), new wijmo.grid.CellRange(6, 3, 6, 3),
        new wijmo.grid.CellRange(10, 1, 10, 1), new wijmo.grid.CellRange(10, 3, 10, 3),
        new wijmo.grid.CellRange(14, 1, 14, 1), new wijmo.grid.CellRange(14, 3, 14, 3),
        new wijmo.grid.CellRange(18, 1, 18, 1), new wijmo.grid.CellRange(18, 3, 18, 3),
        new wijmo.grid.CellRange(22, 1, 22, 1), new wijmo.grid.CellRange(22, 3, 22, 3),
        new wijmo.grid.CellRange(26, 1, 27, 1)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 1, 0, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 1, 9, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(13, 1, 13, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(17, 1, 17, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(21, 1, 21, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(25, 1, 25, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(26, 2, 26, 4));
}

function generateTextSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Char");
    flexSheet.setCellData(1, 1, "Returns the character specified by the code number.");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "char(65)");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=char(65)");
    flexSheet.setCellData(4, 1, "2. Code");
    flexSheet.setCellData(5, 1, "Returns a numeric code for the first character in a text string.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "code(\"a\")");
    flexSheet.setCellData(6, 3, "Result:");
    flexSheet.setCellData(6, 4, "=code(\"a\")");
    flexSheet.setCellData(8, 1, "3. Concatenate");
    flexSheet.setCellData(9, 1, "Joins several text items into one text item.");
    flexSheet.setCellData(10, 1, "Sample:");
    flexSheet.setCellData(10, 2, "concatenate(\"Hello \", \"World!\")");
    flexSheet.setCellData(11, 1, "Result:");
    flexSheet.setCellData(11, 2, "=concatenate(\"Hello \", \"World!\")");
    flexSheet.setCellData(13, 1, "4. Left");
    flexSheet.setCellData(14, 1, "Returns the leftmost characters from a text value.");
    flexSheet.setCellData(15, 1, "Sample:");
    flexSheet.setCellData(15, 2, "left(\"Abcdef\",3)");
    flexSheet.setCellData(15, 3, "Result:");
    flexSheet.setCellData(15, 4, "=left(\"Abcdef\",3)");
    flexSheet.setCellData(17, 1, "5. Right");
    flexSheet.setCellData(18, 1, "Returns the rightmost characters from a text value.");
    flexSheet.setCellData(19, 1, "Sample:");
    flexSheet.setCellData(19, 2, "right(\"Abcdef\",3)");
    flexSheet.setCellData(19, 3, "Result:");
    flexSheet.setCellData(19, 4, "=right(\"Abcdef\",3)");
    flexSheet.setCellData(21, 1, "6. Mid");
    flexSheet.setCellData(22, 1, "Returns a specific number of characters from a text string starting at the position you specify.");
    flexSheet.setCellData(23, 1, "Sample:");
    flexSheet.setCellData(23, 2, "mid(\"Abcdef\",3,2)");
    flexSheet.setCellData(24, 1, "Result:");
    flexSheet.setCellData(24, 2, "=mid(\"Abcdef\",3,2)");
    flexSheet.setCellData(26, 1, "7. Len");
    flexSheet.setCellData(27, 1, "Returns the number of characters in a text string.");
    flexSheet.setCellData(28, 1, "Sample:");
    flexSheet.setCellData(28, 2, "len(\"Abcdef\")");
    flexSheet.setCellData(28, 3, "Result:");
    flexSheet.setCellData(28, 4, "=len(\"Abcdef\")");
    flexSheet.setCellData(30, 1, "8. Find");
    flexSheet.setCellData(31, 1, "Finds one text value within another (case-sensitive).");
    flexSheet.setCellData(32, 1, "Sample:");
    flexSheet.setCellData(32, 2, "find(\"Bc\",\"ABcdef\")");
    flexSheet.setCellData(33, 1, "Result:");
    flexSheet.setCellData(33, 2, "=find(\"Bc\",\"ABcdef\")");
    flexSheet.setCellData(35, 1, "9. Search");
    flexSheet.setCellData(36, 1, "Finds one text value within another (not case-sensitive).");
    flexSheet.setCellData(37, 1, "Sample:");
    flexSheet.setCellData(37, 2, "search(\"bc\",\"ABcdef\")");
    flexSheet.setCellData(38, 1, "Result:");
    flexSheet.setCellData(38, 2, "=search(\"bc\",\"ABcdef\")");
    flexSheet.setCellData(40, 1, "10. Lower");
    flexSheet.setCellData(41, 1, "Converts text to lowercase.");
    flexSheet.setCellData(42, 1, "Sample:");
    flexSheet.setCellData(42, 2, "lower(\"ABCDE\")");
    flexSheet.setCellData(42, 3, "Result:");
    flexSheet.setCellData(42, 4, "=lower(\"ABCDE\")");
    flexSheet.setCellData(44, 1, "11. Upper");
    flexSheet.setCellData(45, 1, "Converts text to uppercase.");
    flexSheet.setCellData(46, 1, "Sample:");
    flexSheet.setCellData(46, 2, "upper(\"abcdef\")");
    flexSheet.setCellData(46, 3, "Result:");
    flexSheet.setCellData(46, 4, "=upper(\"abcdef\")");
    flexSheet.setCellData(48, 1, "12. Proper");
    flexSheet.setCellData(49, 1, "Capitalizes the first letter in each word of a text value.");
    flexSheet.setCellData(50, 1, "Sample:");
    flexSheet.setCellData(50, 2, "proper(\"abcde\")");
    flexSheet.setCellData(50, 3, "Result:");
    flexSheet.setCellData(50, 4, "=proper(\"abcde\")");
    flexSheet.setCellData(52, 1, "13. Trim");
    flexSheet.setCellData(53, 1, "Removes spaces from text.");
    flexSheet.setCellData(54, 1, "Sample:");
    flexSheet.setCellData(54, 2, "trim(\"   abcde   \")");
    flexSheet.setCellData(54, 3, "Result:");
    flexSheet.setCellData(54, 4, "=trim(\"   abcde   \")");
    flexSheet.setCellData(56, 1, "14. Replace");
    flexSheet.setCellData(57, 1, "Replaces characters within text.");
    flexSheet.setCellData(58, 1, "Sample:");
    flexSheet.setCellData(58, 2, "replace(\"abcdefg\",2,3,\"wxyz\")");
    flexSheet.setCellData(59, 1, "Result:");
    flexSheet.setCellData(59, 2, "=replace(\"abcdefg\",2,3,\"wxyz\")");
    flexSheet.setCellData(61, 1, "15. Substitute");
    flexSheet.setCellData(62, 1, "Substitutes new text for old text in a text string.");
    flexSheet.setCellData(63, 1, "Sample:");
    flexSheet.setCellData(63, 2, "substitute(\"abcabcdabcdef\",\"ab\",\"xy\")");
    flexSheet.setCellData(64, 1, "Result:");
    flexSheet.setCellData(64, 2, "=substitute(\"abcabcdabcdef\",\"ab\",\"xy\")");
    flexSheet.setCellData(66, 1, "16. Rept");
    flexSheet.setCellData(67, 1, "Repeats text a given number of times.");
    flexSheet.setCellData(68, 1, "Sample:");
    flexSheet.setCellData(68, 2, "rept(\"abc\",3)");
    flexSheet.setCellData(68, 3, "Result:");
    flexSheet.setCellData(68, 4, "=rept(\"abc\",3)");
    flexSheet.setCellData(70, 1, "17. Text");
    flexSheet.setCellData(71, 1, "Formats a number and converts it to text.");
    flexSheet.setCellData(72, 1, "Sample:");
    flexSheet.setCellData(72, 2, "text(1234,\"c2\")");
    flexSheet.setCellData(72, 3, "Result:");
    flexSheet.setCellData(72, 4, "=text(1234,\"c2\")");
    flexSheet.setCellData(74, 1, "18. Value");
    flexSheet.setCellData(75, 1, "Converts a text argument to a number.");
    flexSheet.setCellData(76, 1, "Sample:");
    flexSheet.setCellData(76, 2, "value(\"1234\")");
    flexSheet.setCellData(76, 3, "Result:");
    flexSheet.setCellData(76, 4, "=value(\"1234\")");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 4, 1),
        new wijmo.grid.CellRange(8, 1, 8, 1),
        new wijmo.grid.CellRange(13, 1, 13, 1),
        new wijmo.grid.CellRange(17, 1, 17, 1),
        new wijmo.grid.CellRange(21, 1, 21, 1),
        new wijmo.grid.CellRange(26, 1, 26, 1),
        new wijmo.grid.CellRange(30, 1, 30, 1),
        new wijmo.grid.CellRange(35, 1, 35, 1),
        new wijmo.grid.CellRange(40, 1, 40, 1),
        new wijmo.grid.CellRange(44, 1, 44, 1),
        new wijmo.grid.CellRange(48, 1, 48, 1),
        new wijmo.grid.CellRange(52, 1, 52, 1),
        new wijmo.grid.CellRange(56, 1, 56, 1),
        new wijmo.grid.CellRange(61, 1, 61, 1),
        new wijmo.grid.CellRange(66, 1, 66, 1),
        new wijmo.grid.CellRange(70, 1, 70, 1),
        new wijmo.grid.CellRange(74, 1, 74, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 6, 1), new wijmo.grid.CellRange(6, 3, 6, 3),
        new wijmo.grid.CellRange(10, 1, 11, 1),
        new wijmo.grid.CellRange(15, 1, 15, 1), new wijmo.grid.CellRange(15, 3, 15, 3),
        new wijmo.grid.CellRange(19, 1, 19, 1), new wijmo.grid.CellRange(19, 3, 19, 3),
        new wijmo.grid.CellRange(23, 1, 24, 1),
        new wijmo.grid.CellRange(28, 1, 28, 1), new wijmo.grid.CellRange(28, 3, 28, 3),
        new wijmo.grid.CellRange(32, 1, 33, 1),
        new wijmo.grid.CellRange(37, 1, 38, 1),
        new wijmo.grid.CellRange(42, 1, 42, 1), new wijmo.grid.CellRange(42, 3, 42, 3),
        new wijmo.grid.CellRange(46, 1, 46, 1), new wijmo.grid.CellRange(46, 3, 46, 3),
        new wijmo.grid.CellRange(50, 1, 50, 1), new wijmo.grid.CellRange(50, 3, 50, 3),
        new wijmo.grid.CellRange(54, 1, 54, 1), new wijmo.grid.CellRange(54, 3, 54, 3),
        new wijmo.grid.CellRange(58, 1, 59, 1),
        new wijmo.grid.CellRange(63, 1, 64, 1),
        new wijmo.grid.CellRange(68, 1, 68, 1), new wijmo.grid.CellRange(68, 3, 68, 3),
        new wijmo.grid.CellRange(72, 1, 72, 1), new wijmo.grid.CellRange(72, 3, 72, 3),
        new wijmo.grid.CellRange(76, 1, 76, 1), new wijmo.grid.CellRange(76, 3, 76, 3)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(8, 1, 8, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 1, 9, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(10, 2, 10, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(14, 1, 14, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(18, 1, 18, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(22, 1, 22, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(23, 2, 23, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(27, 1, 27, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(31, 1, 31, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(32, 2, 32, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(36, 1, 36, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(37, 2, 37, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(41, 1, 41, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(45, 1, 45, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(49, 1, 49, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(53, 1, 53, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(57, 1, 57, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(58, 2, 58, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(61, 1, 61, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(62, 1, 62, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(63, 2, 63, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(67, 1, 67, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(71, 1, 71, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(75, 1, 75, 3));
}

function generateAggregateSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "sample data:");
    for (var rowIndex = 1; rowIndex <= 4; rowIndex++) {
        for (var colIndex = 1; colIndex <= 7; colIndex++) {
            flexSheet.setCellData(rowIndex, colIndex, Math.random() * 200);
        }
    }
    flexSheet.setCellData(6, 1, "Tree");
    flexSheet.setCellData(6, 2, "Height");
    flexSheet.setCellData(6, 3, "Age");
    flexSheet.setCellData(6, 4, "Yield");
    flexSheet.setCellData(6, 5, "Profit");
    flexSheet.setCellData(6, 6, "Height");
    flexSheet.setCellData(7, 1, "Apple");
    flexSheet.setCellData(7, 2, ">10");
    flexSheet.setCellData(7, 6, "<16");
    flexSheet.setCellData(8, 1, "Pear");
    flexSheet.setCellData(10, 1, "Tree");
    flexSheet.setCellData(10, 2, "Height");
    flexSheet.setCellData(10, 3, "Age");
    flexSheet.setCellData(10, 4, "Yield");
    flexSheet.setCellData(10, 5, "Profit");
    for (var rowIndex = 11; rowIndex <= 20; rowIndex++) {
        for (var colIndex = 1; colIndex <= 5; colIndex++) {
            if (colIndex === 1) {
                if (rowIndex === 13) {
                    flexSheet.setCellData(rowIndex, colIndex, "Apple");
                } else {
                    flexSheet.setCellData(rowIndex, colIndex, ["Apple", "Pear", "Cherry", "Orange"][Math.floor(Math.random() * 4)]);
                }
            } else if (colIndex === 5) {
                flexSheet.setCellData(rowIndex, colIndex, Math.random() * 300);
            } else {
                if (rowIndex === 13 && colIndex === 2) {
                    flexSheet.setCellData(rowIndex, colIndex, 15);
                } else if (rowIndex === 13 && colIndex === 3) {
                    flexSheet.setCellData(rowIndex, colIndex, "N/A");
                } else {
                    flexSheet.setCellData(rowIndex, colIndex, Math.round(Math.random() * 20));
                }
            }
        }
    }
    flexSheet.setCellData(22, 1, "1. Sum");
    flexSheet.setCellData(23, 1, "Adds its arguments.");
    flexSheet.setCellData(24, 1, "Sample:");
    flexSheet.setCellData(24, 2, "sum(B2:D4)");
    flexSheet.setCellData(25, 1, "Result:");
    flexSheet.setCellData(25, 2, "=sum(B2:D4)");
    flexSheet.setCellData(26, 1, "Sample:");
    flexSheet.setCellData(26, 2, "sum(1,3,5,7,10,12,13)");
    flexSheet.setCellData(27, 1, "Result:");
    flexSheet.setCellData(27, 2, "=sum(1,3,5,7,10,12,13)");
    flexSheet.setCellData(29, 1, "2. Average");
    flexSheet.setCellData(30, 1, "Returns the average of its arguments.");
    flexSheet.setCellData(31, 1, "Sample:");
    flexSheet.setCellData(31, 2, "average(C2:E3)");
    flexSheet.setCellData(32, 1, "Result:");
    flexSheet.setCellData(32, 2, "=average(C2:E3)");
    flexSheet.setCellData(33, 1, "Sample:");
    flexSheet.setCellData(33, 2, "average(2,4,5,7,11,13,19)");
    flexSheet.setCellData(34, 1, "Result:");
    flexSheet.setCellData(34, 2, "=average(2,4,5,7,11,13,19)");
    flexSheet.setCellData(36, 1, "3. Count");
    flexSheet.setCellData(37, 1, "Counts how many numbers are in the list of arguments.");
    flexSheet.setCellData(38, 1, "Sample:");
    flexSheet.setCellData(38, 2, "count(B3:E4)");
    flexSheet.setCellData(39, 1, "Result:");
    flexSheet.setCellData(39, 2, "=count(B3:E4)");
    flexSheet.setCellData(40, 1, "Sample:");
    flexSheet.setCellData(40, 2, "count(1,7,8,10,11,16,19)");
    flexSheet.setCellData(41, 1, "Result:");
    flexSheet.setCellData(41, 2, "=count(1,7,8,10,11,16,19)");
    flexSheet.setCellData(43, 1, "4. Max");
    flexSheet.setCellData(44, 1, "Returns the maximum value in a list of arguments.");
    flexSheet.setCellData(45, 1, "Sample:");
    flexSheet.setCellData(45, 2, "max(C3:F5)");
    flexSheet.setCellData(46, 1, "Result:");
    flexSheet.setCellData(46, 2, "=max(C3:F5)");
    flexSheet.setCellData(47, 1, "Sample:");
    flexSheet.setCellData(47, 2, "max(100,87,103,54,75,34)");
    flexSheet.setCellData(48, 1, "Result:");
    flexSheet.setCellData(48, 2, "=max(100,87,103,54,75,34)");
    flexSheet.setCellData(50, 1, "5. Min");
    flexSheet.setCellData(51, 1, "Returns the minimum value in a list of arguments.");
    flexSheet.setCellData(52, 1, "Sample:");
    flexSheet.setCellData(52, 2, "min(B2:G5)");
    flexSheet.setCellData(53, 1, "Result:");
    flexSheet.setCellData(53, 2, "=min(B2:G5)");
    flexSheet.setCellData(54, 1, "Sample:");
    flexSheet.setCellData(54, 2, "min(74,47,68,99,106,13,51)");
    flexSheet.setCellData(55, 1, "Result:");
    flexSheet.setCellData(55, 2, "=min(74,47,68,99,106,13,51)");
    flexSheet.setCellData(57, 1, "6. StDev");
    flexSheet.setCellData(58, 1, "Estimates standard deviation based on a sample.");
    flexSheet.setCellData(59, 1, "Sample:");
    flexSheet.setCellData(59, 2, "stdev(B3:G5)");
    flexSheet.setCellData(60, 1, "Result:");
    flexSheet.setCellData(60, 2, "=stdev(B3:G5)");
    flexSheet.setCellData(61, 1, "Sample:");
    flexSheet.setCellData(61, 2, "stdev(74,47,68,99,106,13,51)");
    flexSheet.setCellData(62, 1, "Result:");
    flexSheet.setCellData(62, 2, "=stdev(74,47,68,99,106,13,51)");
    flexSheet.setCellData(64, 1, "7. StDevP");
    flexSheet.setCellData(65, 1, "Calculates standard deviation based on the entire population.");
    flexSheet.setCellData(66, 1, "Sample:");
    flexSheet.setCellData(66, 2, "stdevp(B3:G5)");
    flexSheet.setCellData(67, 1, "Result:");
    flexSheet.setCellData(67, 2, "=stdevp(B3:G5)");
    flexSheet.setCellData(68, 1, "Sample:");
    flexSheet.setCellData(68, 2, "stdevp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(69, 1, "Result:");
    flexSheet.setCellData(69, 2, "=stdevp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(71, 1, "8. Var");
    flexSheet.setCellData(72, 1, "Estimates variance based on a sample.");
    flexSheet.setCellData(73, 1, "Sample:");
    flexSheet.setCellData(73, 2, "var(C2:H4)");
    flexSheet.setCellData(74, 1, "Result:");
    flexSheet.setCellData(74, 2, "=var(C2:H4)");
    flexSheet.setCellData(75, 1, "Sample:");
    flexSheet.setCellData(75, 2, "var(74,47,68,99,106,13,51)");
    flexSheet.setCellData(76, 1, "Result:");
    flexSheet.setCellData(76, 2, "=var(74,47,68,99,106,13,51)");
    flexSheet.setCellData(78, 1, "9. VarP");
    flexSheet.setCellData(79, 1, "Calculates variance based on the entire population.");
    flexSheet.setCellData(80, 1, "Sample:");
    flexSheet.setCellData(80, 2, "varp(C2:H4)");
    flexSheet.setCellData(81, 1, "Result:");
    flexSheet.setCellData(81, 2, "=varp(C2:H4)");
    flexSheet.setCellData(82, 1, "Sample:");
    flexSheet.setCellData(82, 2, "varp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(83, 1, "Result:");
    flexSheet.setCellData(83, 2, "=varp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(85, 1, "10. CountA");
    flexSheet.setCellData(86, 1, "Counts how many values are in the list of arguments.");
    flexSheet.setCellData(87, 1, "Sample:");
    flexSheet.setCellData(87, 2, "counta(E7:E21)");
    flexSheet.setCellData(88, 1, "Result:");
    flexSheet.setCellData(88, 2, "=counta(E7:E21)");
    flexSheet.setCellData(90, 1, "11. CountBlank");
    flexSheet.setCellData(91, 1, "Counts the number of blank cells within a range.");
    flexSheet.setCellData(92, 1, "Sample:");
    flexSheet.setCellData(92, 2, "countblank(E7:E21)");
    flexSheet.setCellData(93, 1, "Result:");
    flexSheet.setCellData(93, 2, "=countblank(E7:E21)");
    flexSheet.setCellData(95, 1, "12. CountIf");
    flexSheet.setCellData(96, 1, "Counts the number of cells within a range that meet the given criteria.");
    flexSheet.setCellData(97, 1, "Syntax:");
    flexSheet.setCellData(97, 2, "countif(range, criteria)");
    flexSheet.setCellData(98, 1, "Sample:");
    flexSheet.setCellData(98, 2, "countif(B12:B21, \"Apple\")");
    flexSheet.setCellData(99, 1, "Result:");
    flexSheet.setCellData(99, 2, "=countif(B12:B21, \"Apple\")");
    flexSheet.setCellData(100, 1, "Sample:");
    flexSheet.setCellData(100, 2, "countif(C12:C21, \">10\")");
    flexSheet.setCellData(101, 1, "Result:");
    flexSheet.setCellData(101, 2, "=countif(C12:C21, \">10\")");
    flexSheet.setCellData(103, 1, "13. CountIfs");
    flexSheet.setCellData(104, 1, "Counts the number of cells within a range that meet multiple criteria.");
    flexSheet.setCellData(105, 1, "Syntax:");
    flexSheet.setCellData(105, 2, "countifs(criteria_range1, criteria1, [criteria_range2, criteria2],...)");
    flexSheet.setCellData(106, 1, "Sample:");
    flexSheet.setCellData(106, 2, "countifs(B12:B21, \"Apple\", C12:C21, \">10\")");
    flexSheet.setCellData(107, 1, "Result:");
    flexSheet.setCellData(107, 2, "=countifs(B12:B21, \"Apple\", C12:C21, \">10\")");
    flexSheet.setCellData(109, 1, "14. DCount");
    flexSheet.setCellData(110, 1, "Counts the cells that contain numbers in a database.");
    flexSheet.setCellData(111, 1, "Syntax:");
    flexSheet.setCellData(111, 2, "countifs(count_range, field, criteria_range)");
    flexSheet.setCellData(112, 1, "Sample:");
    flexSheet.setCellData(112, 2, "dcount(B11:F21, \"Age\", B7:G9)");
    flexSheet.setCellData(113, 1, "Result:");
    flexSheet.setCellData(113, 2, "=dcount(B11:F21, \"Age\", B7:G9)");
    flexSheet.setCellData(115, 1, "15. SumIf");
    flexSheet.setCellData(116, 1, "Adds the cells specified by a given criteria.");
    flexSheet.setCellData(117, 1, "Syntax:");
    flexSheet.setCellData(117, 2, "sumif(range, criteria, [sum_range])");
    flexSheet.setCellData(118, 1, "Remarks:");
    flexSheet.setCellData(118, 2, "If the sum_range argument is omitted, FlexSheet adds the cells that are specified in the range argument.");
    flexSheet.setCellData(119, 1, "Sample:");
    flexSheet.setCellData(119, 2, "sumif(B12:B21, \"Apple\", C12:C21)");
    flexSheet.setCellData(120, 1, "Result:");
    flexSheet.setCellData(120, 2, "=sumif(B12:B21, \"Apple\", C12:C21)");
    flexSheet.setCellData(121, 1, "Sample:");
    flexSheet.setCellData(121, 2, "sumif(C12:C21, \">10\")");
    flexSheet.setCellData(122, 1, "Result:");
    flexSheet.setCellData(122, 2, "=sumif(C12:C21, \">10\")");
    flexSheet.setCellData(124, 1, "16. SumIfs");
    flexSheet.setCellData(125, 1, "Adds the cells in a range that meet multiple criteria.");
    flexSheet.setCellData(126, 1, "Syntax:");
    flexSheet.setCellData(126, 2, "sumifs(sum_range, criteria_range1, criteria1, [criteria_range2, criteria2],...)");
    flexSheet.setCellData(127, 1, "Sample:");
    flexSheet.setCellData(127, 2, "sumifs(F12:F21, B12:B21, \"Apple\", C12:C21, \">10\")");
    flexSheet.setCellData(128, 1, "Result:");
    flexSheet.setCellData(128, 2, "=sumifs(F12:F21, B12:B21, \"Apple\", C12:C21, \">10\")");
    flexSheet.setCellData(130, 1, "17. Rank");
    flexSheet.setCellData(131, 1, "Returns the rank of a number in a list of numbers.");
    flexSheet.setCellData(132, 1, "Syntax:");
    flexSheet.setCellData(132, 2, "rank(number, ref, [order])");
    flexSheet.setCellData(133, 1, "Remarks:");
    flexSheet.setCellData(133, 2, "If order is 0 (zero) or omitted, FlexSheet ranks number as if ref were a list sorted in descending order.");
    flexSheet.setCellData(134, 2, "If order is any nonzero value, FlexSheet ranks number as if ref were a list sorted in ascending order.");
    flexSheet.setCellData(135, 1, "Sample:");
    flexSheet.setCellData(135, 2, "rank(15, C12:C21)");
    flexSheet.setCellData(136, 1, "Result:");
    flexSheet.setCellData(136, 2, "=rank(15, C12:C21)");
    flexSheet.setCellData(137, 1, "Sample:");
    flexSheet.setCellData(137, 2, "rank(15, C12:C21, 1)");
    flexSheet.setCellData(138, 1, "Result:");
    flexSheet.setCellData(138, 2, "=rank(15, C12:C21, 1)");
    flexSheet.setCellData(140, 1, "18. Product");
    flexSheet.setCellData(141, 1, "Multiplies its arguments.");
    flexSheet.setCellData(142, 1, "Sample:");
    flexSheet.setCellData(142, 2, "product(C12:E12)");
    flexSheet.setCellData(143, 1, "Result:");
    flexSheet.setCellData(143, 2, "=product(C12:E12)");
    flexSheet.setCellData(144, 1, "Sample:");
    flexSheet.setCellData(144, 2, "product(1, 2, 3, 4, 5)");
    flexSheet.setCellData(145, 1, "Result:");
    flexSheet.setCellData(145, 2, "=product(1, 2, 3, 4, 5)");
    flexSheet.setCellData(147, 1, "19. Subtotal");
    flexSheet.setCellData(148, 1, "Returns a subtotal in a list or database.");
    flexSheet.setCellData(149, 1, "Syntax:");
    flexSheet.setCellData(149, 2, "subtotal(function_num, ref1, [ref2],...)");
    flexSheet.setCellData(150, 1, "Remarks:");
    flexSheet.setCellData(150, 2, "The function_num 1-11 or 101-111 that specifies the function to use for the subtotal.");
    flexSheet.setCellData(151, 2, "1-11 includes manually-hidden rows, while 101-111 excludes them.");
    flexSheet.setCellData(152, 2, "Function_Num");
    flexSheet.setCellData(152, 4, "Function_Num");
    flexSheet.setCellData(152, 6, "Function");
    flexSheet.setCellData(153, 2, "(includes hidden values)");
    flexSheet.setCellData(153, 4, "(ignores hidden values)");
    flexSheet.setCellData(154, 2, "1");
    flexSheet.setCellData(154, 4, "101");
    flexSheet.setCellData(154, 6, "Average");
    flexSheet.setCellData(155, 2, "2");
    flexSheet.setCellData(155, 4, "102");
    flexSheet.setCellData(155, 6, "Count");
    flexSheet.setCellData(156, 2, "3");
    flexSheet.setCellData(156, 4, "103");
    flexSheet.setCellData(156, 6, "CountA");
    flexSheet.setCellData(157, 2, "4");
    flexSheet.setCellData(157, 4, "104");
    flexSheet.setCellData(157, 6, "Max");
    flexSheet.setCellData(158, 2, "5");
    flexSheet.setCellData(158, 4, "105");
    flexSheet.setCellData(158, 6, "Min");
    flexSheet.setCellData(159, 2, "6");
    flexSheet.setCellData(159, 4, "106");
    flexSheet.setCellData(159, 6, "Product");
    flexSheet.setCellData(160, 2, "7");
    flexSheet.setCellData(160, 4, "107");
    flexSheet.setCellData(160, 6, "Stdev");
    flexSheet.setCellData(161, 2, "8");
    flexSheet.setCellData(161, 4, "108");
    flexSheet.setCellData(161, 6, "StdevP");
    flexSheet.setCellData(162, 2, "9");
    flexSheet.setCellData(162, 4, "109");
    flexSheet.setCellData(162, 6, "Sum");
    flexSheet.setCellData(163, 2, "10");
    flexSheet.setCellData(163, 4, "110");
    flexSheet.setCellData(163, 6, "Var");
    flexSheet.setCellData(164, 2, "11");
    flexSheet.setCellData(164, 4, "111");
    flexSheet.setCellData(164, 6, "VarP");
    flexSheet.setCellData(165, 1, "Sample:");
    flexSheet.setCellData(165, 2, "subtotal(3, B7:D9, G7:G10)");
    flexSheet.setCellData(166, 1, "Result:");
    flexSheet.setCellData(166, 2, "=subtotal(3, B7:D9, G7:G10)");
    flexSheet.setCellData(167, 1, "Sample:");
    flexSheet.setCellData(167, 2, "subtotal(6, E12:F12)");
    flexSheet.setCellData(168, 1, "Result:");
    flexSheet.setCellData(168, 2, "=subtotal(6, E12:F12)");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(22, 1, 22, 1),
        new wijmo.grid.CellRange(29, 1, 29, 1),
        new wijmo.grid.CellRange(36, 1, 36, 1),
        new wijmo.grid.CellRange(43, 1, 43, 1),
        new wijmo.grid.CellRange(50, 1, 50, 1),
        new wijmo.grid.CellRange(57, 1, 57, 1),
        new wijmo.grid.CellRange(64, 1, 64, 1),
        new wijmo.grid.CellRange(71, 1, 71, 1),
        new wijmo.grid.CellRange(78, 1, 78, 1),
        new wijmo.grid.CellRange(85, 1, 85, 1),
        new wijmo.grid.CellRange(90, 1, 90, 2),
        new wijmo.grid.CellRange(95, 1, 95, 1),
        new wijmo.grid.CellRange(103, 1, 103, 1),
        new wijmo.grid.CellRange(109, 1, 109, 1),
        new wijmo.grid.CellRange(115, 1, 115, 1),
        new wijmo.grid.CellRange(124, 1, 124, 1),
        new wijmo.grid.CellRange(130, 1, 130, 1),
        new wijmo.grid.CellRange(140, 1, 140, 1),
        new wijmo.grid.CellRange(147, 1, 147, 1),
        new wijmo.grid.CellRange(152, 2, 153, 6)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(24, 1, 27, 1),
        new wijmo.grid.CellRange(31, 1, 34, 1),
        new wijmo.grid.CellRange(38, 1, 41, 1),
        new wijmo.grid.CellRange(45, 1, 48, 1),
        new wijmo.grid.CellRange(52, 1, 55, 1),
        new wijmo.grid.CellRange(59, 1, 62, 1),
        new wijmo.grid.CellRange(66, 1, 69, 1),
        new wijmo.grid.CellRange(73, 1, 76, 1),
        new wijmo.grid.CellRange(80, 1, 83, 1),
        new wijmo.grid.CellRange(87, 1, 88, 1),
        new wijmo.grid.CellRange(92, 1, 93, 1),
        new wijmo.grid.CellRange(97, 1, 101, 1),
        new wijmo.grid.CellRange(105, 1, 107, 1),
        new wijmo.grid.CellRange(111, 1, 113, 1),
        new wijmo.grid.CellRange(117, 1, 122, 1),
        new wijmo.grid.CellRange(126, 1, 128, 1),
        new wijmo.grid.CellRange(132, 1, 133, 1), new wijmo.grid.CellRange(135, 1, 138, 1),
        new wijmo.grid.CellRange(142, 1, 145, 1),
        new wijmo.grid.CellRange(149, 1, 150, 1), new wijmo.grid.CellRange(165, 1, 168, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'center',
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(6, 1, 6, 6),
        new wijmo.grid.CellRange(10, 1, 10, 5)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(23, 1, 23, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(26, 2, 26, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(30, 1, 30, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(33, 2, 33, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(37, 1, 37, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(40, 2, 40, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(42, 1, 42, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(44, 1, 44, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(47, 2, 47, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(51, 1, 51, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(54, 2, 54, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(58, 1, 58, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(61, 2, 61, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(65, 1, 65, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(68, 2, 68, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(72, 1, 72, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(75, 2, 75, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(79, 1, 79, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(82, 2, 82, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(86, 1, 86, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(90, 1, 90, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(91, 1, 91, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(92, 2, 92, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(96, 1, 96, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(97, 2, 97, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(98, 2, 98, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(100, 2, 100, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(104, 1, 104, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(105, 2, 105, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(106, 2, 106, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(110, 1, 110, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(111, 2, 111, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(112, 2, 112, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(116, 1, 116, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(117, 2, 117, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(118, 2, 118, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(119, 2, 119, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(121, 2, 121, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(125, 1, 125, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(126, 2, 126, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(127, 2, 127, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(131, 1, 131, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(132, 2, 132, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(133, 2, 133, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(134, 2, 134, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(135, 2, 135, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(137, 2, 137, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(141, 1, 141, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(142, 2, 142, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(144, 2, 144, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(148, 1, 148, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(149, 2, 149, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(150, 2, 150, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(151, 2, 151, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(152, 2, 152, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(152, 4, 152, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(153, 2, 153, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(153, 4, 153, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(165, 2, 165, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(167, 2, 167, 3));
}

function generateDateSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Now");
    flexSheet.setCellData(1, 1, "Returns the serial number of the current date and time.");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "Now()");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=Now()");
    flexSheet.setCellData(4, 1, "2. Year");
    flexSheet.setCellData(5, 1, "Converts a serial number to a year.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "Year(E3)");
    flexSheet.setCellData(6, 3, "Result:");
    flexSheet.setCellData(6, 4, "=Year(E3)");
    flexSheet.setCellData(8, 1, "3. Month");
    flexSheet.setCellData(9, 1, "Converts a serial number to a month.");
    flexSheet.setCellData(10, 1, "Sample:");
    flexSheet.setCellData(10, 2, "Month(E3)");
    flexSheet.setCellData(10, 3, "Result:");
    flexSheet.setCellData(10, 4, "=Month(E3)");
    flexSheet.setCellData(12, 1, "4. Day");
    flexSheet.setCellData(13, 1, "Converts a serial number to a day of the month.");
    flexSheet.setCellData(14, 1, "Sample:");
    flexSheet.setCellData(14, 2, "Day(E3)");
    flexSheet.setCellData(14, 3, "Result:");
    flexSheet.setCellData(14, 4, "=Day(E3)");
    flexSheet.setCellData(16, 1, "5. Today");
    flexSheet.setCellData(17, 1, "Returns the serial number of today's date.");
    flexSheet.setCellData(18, 1, "Sample:");
    flexSheet.setCellData(18, 2, "today()");
    flexSheet.setCellData(18, 3, "Result:");
    flexSheet.setCellData(18, 4, "=today()");
    flexSheet.setCellData(20, 1, "6. Date");
    flexSheet.setCellData(21, 1, "Returns the serial number of a particular date.");
    flexSheet.setCellData(22, 1, "Sample:");
    flexSheet.setCellData(22, 2, "date(2015, 11, 26)");
    flexSheet.setCellData(23, 1, "Result:");
    flexSheet.setCellData(23, 2, "=date(2015, 11, 26)");
    flexSheet.setCellData(25, 1, "7. Time");
    flexSheet.setCellData(26, 1, "Returns the serial number of a particular time.");
    flexSheet.setCellData(27, 1, "Sample:");
    flexSheet.setCellData(27, 2, "time(11, 28, 33)");
    flexSheet.setCellData(28, 1, "Result:");
    flexSheet.setCellData(28, 2, "=time(11, 28, 33)");
    flexSheet.setCellData(30, 1, "8. Hour");
    flexSheet.setCellData(31, 1, "Converts a serial number to an hour.");
    flexSheet.setCellData(32, 1, "Sample:");
    flexSheet.setCellData(32, 2, "hour(C29)");
    flexSheet.setCellData(32, 3, "Result:");
    flexSheet.setCellData(32, 4, "=hour(C29)");
    flexSheet.setCellData(33, 1, "Sample:");
    flexSheet.setCellData(33, 2, "hour(0.65)");
    flexSheet.setCellData(33, 3, "Result:");
    flexSheet.setCellData(33, 4, "=hour(0.65)");
    flexSheet.setCellData(35, 1, "9. DateDif");
    flexSheet.setCellData(36, 1, "Calculates the number of days, months, or years between two dates.");
    flexSheet.setCellData(37, 1, "Syntax:");
    flexSheet.setCellData(37, 2, "DateDif(start_date, end_date, unit)");
    flexSheet.setCellData(38, 1, "The unit paratemer can be following values:");
    flexSheet.setCellData(39, 1, "\"Y\"");
    flexSheet.setCellData(39, 2, "The number of complete years in the period.");
    flexSheet.setCellData(40, 1, "\"M\"");
    flexSheet.setCellData(40, 2, "The number of complete months in the period.");
    flexSheet.setCellData(41, 1, "\"D\"");
    flexSheet.setCellData(41, 2, "The number of days in the period.");
    flexSheet.setCellData(42, 1, "\"MD\"");
    flexSheet.setCellData(42, 2, "The difference between the days in start_date and end_date. The months and years of the dates are ignored.");
    flexSheet.setCellData(43, 1, "\"YM\"");
    flexSheet.setCellData(43, 2, "The difference between the months in start_date and end_date. The days and years of the dates are ignored.");
    flexSheet.setCellData(44, 1, "\"YD\"");
    flexSheet.setCellData(44, 2, "The difference between the days of start_date and end_date. The years of the dates are ignored.");
    flexSheet.setCellData(45, 1, "Sample:");
    flexSheet.setCellData(45, 2, "datedif(\"11/26/2012\", \"8/15/2015\", \"Y\")");
    flexSheet.setCellData(46, 1, "Result:");
    flexSheet.setCellData(46, 2, "=datedif(\"11/26/2012\", \"8/15/2015\", \"Y\")");
    flexSheet.setCellData(47, 1, "Sample:");
    flexSheet.setCellData(47, 2, "datedif(\"5/26/2015\", \"11/15/2015\", \"M\")");
    flexSheet.setCellData(48, 1, "Result:");
    flexSheet.setCellData(48, 2, "=datedif(\"5/26/2015\", \"11/15/2015\", \"M\")");
    flexSheet.setCellData(49, 1, "Sample:");
    flexSheet.setCellData(49, 2, "datedif(\"2/26/2014\", \"3/15/2015\", \"D\")");
    flexSheet.setCellData(50, 1, "Result:");
    flexSheet.setCellData(50, 2, "=datedif(\"2/26/2014\", \"3/15/2015\", \"D\")");
    flexSheet.setCellData(51, 1, "Sample:");
    flexSheet.setCellData(51, 2, "datedif(\"3/26/2015\", \"2/15/2016\", \"MD\")");
    flexSheet.setCellData(52, 1, "Result:");
    flexSheet.setCellData(52, 2, "=datedif(\"3/26/2015\", \"2/15/2016\", \"MD\")");
    flexSheet.setCellData(53, 1, "Sample:");
    flexSheet.setCellData(53, 2, "datedif(\"11/26/2015\", \"2/15/2016\", \"YM\")");
    flexSheet.setCellData(54, 1, "Result:");
    flexSheet.setCellData(54, 2, "=datedif(\"11/26/2015\", \"2/15/2016\", \"YM\")");
    flexSheet.setCellData(55, 1, "Sample:");
    flexSheet.setCellData(55, 2, "datedif(\"2/26/2016\", \"2/15/2017\", \"YD\")");
    flexSheet.setCellData(56, 1, "Result:");
    flexSheet.setCellData(56, 2, "=datedif(\"2/26/2016\", \"2/15/2017\", \"YD\")");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 4, 1),
        new wijmo.grid.CellRange(8, 1, 8, 1),
        new wijmo.grid.CellRange(12, 1, 12, 1),
        new wijmo.grid.CellRange(16, 1, 16, 1),
        new wijmo.grid.CellRange(20, 1, 20, 1),
        new wijmo.grid.CellRange(25, 1, 25, 1),
        new wijmo.grid.CellRange(30, 1, 30, 1),
        new wijmo.grid.CellRange(35, 1, 35, 1),
        new wijmo.grid.CellRange(39, 1, 44, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 6, 1), new wijmo.grid.CellRange(6, 3, 6, 3),
        new wijmo.grid.CellRange(10, 1, 10, 1), new wijmo.grid.CellRange(10, 3, 10, 3),
        new wijmo.grid.CellRange(14, 1, 14, 1), new wijmo.grid.CellRange(14, 3, 14, 3),
        new wijmo.grid.CellRange(18, 1, 18, 1), new wijmo.grid.CellRange(18, 3, 18, 3),
        new wijmo.grid.CellRange(22, 1, 23, 1),
        new wijmo.grid.CellRange(27, 1, 28, 1),
        new wijmo.grid.CellRange(32, 1, 33, 1), new wijmo.grid.CellRange(32, 3, 33, 3),
        new wijmo.grid.CellRange(37, 1, 37, 1),
        new wijmo.grid.CellRange(39, 1, 56, 1)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(2, 4, 2, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 1, 9, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(13, 1, 13, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(17, 1, 17, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(21, 1, 21, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(22, 2, 22, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(26, 1, 26, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(27, 2, 27, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(31, 1, 31, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(36, 1, 36, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(37, 2, 37, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(38, 1, 38, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(39, 2, 39, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(40, 2, 40, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(41, 2, 41, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(42, 2, 42, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(43, 2, 43, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(44, 2, 44, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(45, 2, 45, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(47, 2, 47, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(49, 2, 49, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(51, 2, 51, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(53, 2, 53, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(55, 2, 55, 4));
}

function generateLookupReferenceSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Cell Reference");
    flexSheet.setCellData(1, 1, "Gets value for a specific cell in the flexsheet.");
    flexSheet.setCellData(2, 1, "Sample:");
    flexSheet.setCellData(2, 2, "B1");
    flexSheet.setCellData(2, 3, "Result:");
    flexSheet.setCellData(2, 4, "=B1");
    flexSheet.setCellData(4, 1, "2. Choose");
    flexSheet.setCellData(5, 1, "Chooses a value from a list of values.");
    flexSheet.setCellData(6, 1, "Sample:");
    flexSheet.setCellData(6, 2, "choose(2, \"Hello\", \"World\", \"for\", \"test\")");
    flexSheet.setCellData(7, 1, "Result:");
    flexSheet.setCellData(7, 2, "=choose(2, \"Hello\", \"World\", \"for\", \"test\")");
    flexSheet.setCellData(9, 1, "3. Column");
    flexSheet.setCellData(10, 1, "Returns the column number of a reference.");
    flexSheet.setCellData(11, 1, "Sample:");
    flexSheet.setCellData(11, 2, "column(E1)");
    flexSheet.setCellData(11, 3, "Result:");
    flexSheet.setCellData(11, 4, "=column(E1)");
    flexSheet.setCellData(13, 1, "4. Columns");
    flexSheet.setCellData(14, 1, "Returns the number of columns in a reference.");
    flexSheet.setCellData(15, 1, "Sample:");
    flexSheet.setCellData(15, 2, "columns(B2:D5)");
    flexSheet.setCellData(15, 3, "Result:");
    flexSheet.setCellData(15, 4, "=columns(B2:D5)");
    flexSheet.setCellData(17, 1, "5. Row");
    flexSheet.setCellData(18, 1, "Returns the row number of a reference.");
    flexSheet.setCellData(19, 1, "Sample:");
    flexSheet.setCellData(19, 2, "row(B21)");
    flexSheet.setCellData(19, 3, "Result:");
    flexSheet.setCellData(19, 4, "=row(B21)");
    flexSheet.setCellData(21, 1, "6. Rows");
    flexSheet.setCellData(22, 1, "Returns the number of rows in a reference.");
    flexSheet.setCellData(23, 1, "Sample:");
    flexSheet.setCellData(23, 2, "rows(B21:E13)");
    flexSheet.setCellData(23, 3, "Result:");
    flexSheet.setCellData(23, 4, "=rows(B21:E13)");
    flexSheet.setCellData(25, 1, "7. Index");
    flexSheet.setCellData(26, 1, "Uses an index to choose a value from a reference.");
    flexSheet.setCellData(27, 1, "Syntax:");
    flexSheet.setCellData(27, 2, "index(range,row_num,[col_num])");
    flexSheet.setCellData(28, 1, "Remarks:");
    flexSheet.setCellData(28, 2, "If row_num or column_num to 0, inedx returns the array of values for the entire column or row.");
    flexSheet.setCellData(29, 1, "Sample:");
    flexSheet.setCellData(29, 2, "index(B46:F49, 2, 2)");
    flexSheet.setCellData(30, 1, "Result:");
    flexSheet.setCellData(30, 2, "=index(B46:F49, 2, 2)");
    flexSheet.setCellData(31, 1, "Sample:");
    flexSheet.setCellData(31, 2, "sum(index(C47:D48, 0, 1))");
    flexSheet.setCellData(32, 1, "Result:");
    flexSheet.setCellData(32, 2, "=sum(index(C47:D48, 0, 1))");
    flexSheet.setCellData(34, 1, "8. HLookup");
    flexSheet.setCellData(35, 1, "Looks in the top row of an array and returns the value of the indicated cell.");
    flexSheet.setCellData(36, 1, "Syntax:");
    flexSheet.setCellData(36, 2, "hlookup(lookup_value, range, row_index_num, [range_lookup])");
    flexSheet.setCellData(37, 1, "Remarks:");
    flexSheet.setCellData(37, 2, "range_lookup is a logical value that specifies whether you want HLOOKUP to find an exact match ");
    flexSheet.setCellData(38, 2, "or an approximate match.");
    flexSheet.setCellData(39, 2, "If TRUE or omitted, an approximate match is returned.");
    flexSheet.setCellData(40, 2, "In other words, if an exact match is not found, the next largest value that is less than lookup_value is returned.");
    flexSheet.setCellData(41, 2, "If FALSE, HLOOKUP will find an exact match.");
    flexSheet.setCellData(42, 2, "If range_lookup is FALSE and lookup_value is text, you can use the wildcard characters, ");
    flexSheet.setCellData(43, 2, "question mark (?) and asterisk (*).");
    flexSheet.setCellData(44, 1, "Sample Data:");
    flexSheet.setCellData(45, 1, "4Test");
    flexSheet.setCellData(45, 2, "Test4");
    flexSheet.setCellData(45, 3, "4Test4");
    flexSheet.setCellData(45, 4, "44Test4");
    flexSheet.setCellData(45, 5, "4Test44");
    flexSheet.setCellData(46, 1, "1");
    flexSheet.setCellData(46, 2, "101");
    flexSheet.setCellData(46, 3, "1001");
    flexSheet.setCellData(46, 4, "5001");
    flexSheet.setCellData(46, 5, "10001");
    flexSheet.setCellData(47, 1, "0.1");
    flexSheet.setCellData(47, 2, "0.2");
    flexSheet.setCellData(47, 3, "0.3");
    flexSheet.setCellData(47, 4, "0.5");
    flexSheet.setCellData(47, 5, "0.8");
    flexSheet.setCellData(48, 1, "Sample:");
    flexSheet.setCellData(48, 2, "hlookup(7500, B47:F48, 2)");
    flexSheet.setCellData(49, 1, "Result:");
    flexSheet.setCellData(49, 2, "=hlookup(7500, B47:F48, 2)");
    flexSheet.setCellData(50, 1, "Sample:");
    flexSheet.setCellData(50, 2, "hlookup(\"?test?\", B46:F48, 3, false)");
    flexSheet.setCellData(51, 1, "Result:");
    flexSheet.setCellData(51, 2, "=hlookup(\"?test?\", B46:F48, 3, false)");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 4, 1),
        new wijmo.grid.CellRange(9, 1, 9, 1),
        new wijmo.grid.CellRange(13, 1, 13, 1),
        new wijmo.grid.CellRange(17, 1, 17, 1),
        new wijmo.grid.CellRange(21, 1, 21, 1),
        new wijmo.grid.CellRange(25, 1, 25, 1),
        new wijmo.grid.CellRange(34, 1, 34, 1),
        new wijmo.grid.CellRange(45, 1, 45, 5)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(2, 3, 2, 3),
        new wijmo.grid.CellRange(6, 1, 7, 1),
        new wijmo.grid.CellRange(11, 1, 11, 1), new wijmo.grid.CellRange(11, 3, 11, 3),
        new wijmo.grid.CellRange(15, 1, 15, 1), new wijmo.grid.CellRange(15, 3, 15, 3),
        new wijmo.grid.CellRange(19, 1, 19, 1), new wijmo.grid.CellRange(19, 3, 19, 3),
        new wijmo.grid.CellRange(23, 1, 23, 1), new wijmo.grid.CellRange(23, 3, 23, 3),
        new wijmo.grid.CellRange(27, 1, 32, 1),
        new wijmo.grid.CellRange(36, 1, 37, 1),
        new wijmo.grid.CellRange(48, 1, 51, 1)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 1, 0, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(2, 4, 2, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(6, 2, 6, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(10, 1, 10, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(14, 1, 14, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(18, 1, 18, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(22, 1, 22, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(26, 1, 26, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(27, 2, 27, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(28, 2, 28, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(29, 2, 29, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(31, 2, 31, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(35, 1, 35, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(36, 2, 36, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(37, 2, 37, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(38, 2, 38, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(39, 2, 39, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(40, 2, 40, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(41, 2, 41, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(42, 2, 42, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(43, 2, 43, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(48, 2, 48, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(50, 2, 50, 4));
}

function generateFinancialSheet(flexSheet) {
    flexSheet.setCellData(0, 1, "1. Rate");
    flexSheet.setCellData(1, 1, "Returns the interest rate per period of an annuity.");
    flexSheet.setCellData(2, 1, "Syntax:");
    flexSheet.setCellData(2, 2, "rate(nper, pmt, pv, [fv], [type], [guess])");
    flexSheet.setCellData(3, 1, "The rate function syntax has the following arguments:");
    flexSheet.setCellData(4, 1, "nper:");
    flexSheet.setCellData(4, 2, "The total number of payment periods in an annuity.");
    flexSheet.setCellData(5, 1, "pmt:");
    flexSheet.setCellData(5, 2, "The payment made each period and cannot change over the life of the annuity.");
    flexSheet.setCellData(6, 1, "pv:");
    flexSheet.setCellData(6, 2, "The total amount that a series of future payments is worth now.");
    flexSheet.setCellData(7, 1, "fv:");
    flexSheet.setCellData(7, 2, "The future value, or a cash balance you want to attain after the last payment is made.");
    flexSheet.setCellData(8, 1, "type:");
    flexSheet.setCellData(8, 2, "The number 0 or 1 and indicates when payments are due.");
    flexSheet.setCellData(9, 2, "0 or omitted means at the end of the period.");
    flexSheet.setCellData(10, 2, "1 means at the beginning of the period.");
    flexSheet.setCellData(11, 1, "guess:");
    flexSheet.setCellData(11, 2, "Your guess for what the rate will be.  If you omit guess, it is assumed to be 10 percent.");
    flexSheet.setCellData(12, 1, "Sample:");
    flexSheet.setCellData(12, 2, "rate(48, -200, 8000)");
    flexSheet.setCellData(13, 1, "Result:");
    flexSheet.setCellData(13, 2, "=rate(48, -200, 8000)");

    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 1, 0, 1),
        new wijmo.grid.CellRange(4, 1, 11, 1)]);

    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 1, 2, 1), new wijmo.grid.CellRange(4, 1, 13, 1)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(2, 2, 2, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(3, 1, 3, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(4, 2, 4, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 2, 5, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(6, 2, 6, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(7, 2, 7, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(8, 2, 8, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(9, 2, 9, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(10, 2, 10, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(11, 2, 11, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(12, 2, 12, 3));
}

function generateSummraySheet(flexSheet) {
    flexSheet.setCellData(1, 0, 'Function type');
    flexSheet.setCellData(1, 2, 'Count of Function');
    flexSheet.setCellData(2, 0, 'Basic Operator');
    flexSheet.setCellData(2, 3, 14);
    flexSheet.setCellData(3, 0, 'Math');
    flexSheet.setCellData(3, 3, 21);
    flexSheet.setCellData(4, 0, 'Logical');
    flexSheet.setCellData(4, 3, 6);
    flexSheet.setCellData(5, 0, 'Text');
    flexSheet.setCellData(5, 3, 18);
    flexSheet.setCellData(6, 0, 'Aggregate');
    flexSheet.setCellData(6, 3, 19);
    flexSheet.setCellData(7, 0, 'Date');
    flexSheet.setCellData(7, 3, 9);
    flexSheet.setCellData(8, 0, 'Lookup & Reference');
    flexSheet.setCellData(8, 3, 7);
    flexSheet.setCellData(9, 0, 'Financial');
    flexSheet.setCellData(9, 3, 1);
    flexSheet.setCellData(11, 0, 'Total');
    flexSheet.setCellData(11, 3, '=SUM(D3:D10)');

    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        backgroundColor: '#A7D6FF'
    }, [new wijmo.grid.CellRange(1, 0, 1, 3), new wijmo.grid.CellRange(11, 0, 11, 3)]);
    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(2, 3, 11, 3)]);
    for (var rowIndex = 3; rowIndex < 11; rowIndex++) {
        if (rowIndex % 2 === 1) {
            flexSheet.applyCellsStyle({
                backgroundColor: '#E5F3FF'
            }, [new wijmo.grid.CellRange(rowIndex, 0, rowIndex, 3)]);
        }
    }

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 2, 1, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(8, 0, 8, 1));
}