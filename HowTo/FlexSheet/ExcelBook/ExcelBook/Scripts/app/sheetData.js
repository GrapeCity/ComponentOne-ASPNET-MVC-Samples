// Generate the use case template sheet.
function generateUseCaseTemplateSheet(flexSheet) {
    setContentForUseCaseTemplate(flexSheet);

    applyStyleForUseCaseTemplate(flexSheet);
}

// Set content for the use case template sheet.
function setContentForUseCaseTemplate(flexSheet) {
    flexSheet.setCellData(0, 9, 'For Office Use Only');
    flexSheet.setCellData(1, 1, 'Expense Report');
    flexSheet.setCellData(3, 1, 'PURPOSE:');
    flexSheet.setCellData(3, 2, 'On business');
    flexSheet.setCellData(3, 5, 'Attachment:');
    flexSheet.setCellData(3, 6, 'Yes');
    flexSheet.setCellData(3, 9, 'PAY PERIOD:');
    flexSheet.setCellData(3, 10, 'From');
    flexSheet.setCellData(3, 11, '2015-3-1');
    flexSheet.setCellData(4, 10, 'To');
    flexSheet.setCellData(4, 11, '2015-4-1');
    flexSheet.setCellData(5, 1, 'EMPLOYEE IMFORMATION:');
    flexSheet.setCellData(6, 1, 'Name');
    flexSheet.setCellData(6, 2, 'Robert King');
    flexSheet.setCellData(6, 5, 'Position');
    flexSheet.setCellData(6, 6, 'Sales Representative');
    flexSheet.setCellData(6, 9, 'SSN');
    flexSheet.setCellData(6, 10, 'A12345');
    flexSheet.setCellData(7, 1, 'Department');
    flexSheet.setCellData(7, 2, 'Sales');
    flexSheet.setCellData(7, 5, 'Manager');
    flexSheet.setCellData(7, 6, 'Andrew Fuller');
    flexSheet.setCellData(7, 9, 'Employee ID');
    flexSheet.setCellData(7, 10, 'E123456');
    flexSheet.setCellData(9, 1, 'Date');
    flexSheet.setCellData(9, 2, 'Account');
    flexSheet.setCellData(9, 3, 'Description');
    flexSheet.setCellData(9, 4, 'Hotel');
    flexSheet.setCellData(9, 5, 'Transport');
    flexSheet.setCellData(9, 6, 'Fuel');
    flexSheet.setCellData(9, 7, 'Meals');
    flexSheet.setCellData(9, 8, 'Phone');
    flexSheet.setCellData(9, 9, 'Entertainment');
    flexSheet.setCellData(9, 10, 'Misc');
    flexSheet.setCellData(9, 11, 'Total');
    flexSheet.setCellData(17, 1, 'Total');
    flexSheet.setCellData(18, 10, 'Subtotal');
    flexSheet.setCellData(19, 9, 'Cash Advances');
    flexSheet.setCellData(20, 10, 'Total');
    flexSheet.setCellData(20, 1, 'APPROVED:');
    flexSheet.setCellData(20, 5, 'NOTES:');

    setExpenseData(flexSheet);
}

// set expense detail data for the use case template sheet.
function setExpenseData(flexSheet) {
    var rowIndex,
        colIndex,
        value,
        rowAlpha,
        cellRange;

    for (rowIndex = 10; rowIndex <= 17; rowIndex++) {
        for (colIndex = 1; colIndex <= 11; colIndex++) {
            if (rowIndex === 17) {
                if (colIndex >= 4 && colIndex <= 11) {
                    rowAlpha = wijmo.grid.sheet.FlexSheet.convertNumberToAlpha(colIndex);
                    cellRange = rowAlpha + '11' + ':' + rowAlpha + '17';
                    flexSheet.setCellData(rowIndex, colIndex, '=sum(' + cellRange + ')');
                }
            } else {
                if (colIndex === 11) {
                    cellRange = 'E' + (rowIndex + 1) + ':' + 'K' + (rowIndex + 1);
                    flexSheet.setCellData(rowIndex, colIndex, '=sum(' + cellRange + ')');
                } else if (colIndex >= 4 && colIndex < 11) {
                    value = 200 * Math.random();
                    flexSheet.setCellData(rowIndex, colIndex, value);
                } else if (colIndex === 3) {
                    flexSheet.setCellData(rowIndex, colIndex, 'Visit VIP customers.');
                } else if (colIndex === 2) {
                    flexSheet.setCellData(rowIndex, colIndex, '12345678');
                }
            }
        }
    }

    flexSheet.setCellData(10, 1, '2015-3-1');
    flexSheet.setCellData(11, 1, '2015-3-3');
    flexSheet.setCellData(12, 1, '2015-3-7');
    flexSheet.setCellData(13, 1, '2015-3-11');
    flexSheet.setCellData(14, 1, '2015-3-18');
    flexSheet.setCellData(15, 1, '2015-3-21');
    flexSheet.setCellData(16, 1, '2015-3-27');
    flexSheet.setCellData(18, 11, '=L21-L20');
    flexSheet.setCellData(19, 11, 1000);
    flexSheet.setCellData(20, 11, '=L18');
}

// Apply styles for the use case template sheet.
function applyStyleForUseCaseTemplate(flexSheet) {
    flexSheet.columns[0].width = 10;
    flexSheet.columns[1].width = 100;
    flexSheet.columns[3].width = 230;
    flexSheet.columns[5].width = 95;
    flexSheet.columns[6].width = 130;
    flexSheet.columns[9].width = 105;
    for (var i = 4; i <= 11; i++) {
        flexSheet.columns[i].format = 'c2';
    }
    flexSheet.rows[1].height = 45;
    flexSheet.applyCellsStyle({
        fontStyle: 'italic',
        backgroundColor: '#E1DFDF'
    }, [new wijmo.grid.CellRange(0, 9, 0, 11)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 9, 0, 11));
    flexSheet.applyCellsStyle({
        fontSize: '24px',
        fontWeight: 'bold',
        color: '#696964'
    }, [new wijmo.grid.CellRange(1, 1, 1, 3)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 3));
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        color: '#808097'
    }, [new wijmo.grid.CellRange(3, 1, 3, 1),
        new wijmo.grid.CellRange(3, 5, 3, 5),
        new wijmo.grid.CellRange(3, 9, 3, 9),
        new wijmo.grid.CellRange(5, 1, 5, 2)]);
    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(3, 10, 4, 10),
        new wijmo.grid.CellRange(6, 1, 7, 1),
        new wijmo.grid.CellRange(6, 5, 7, 5),
        new wijmo.grid.CellRange(6, 9, 7, 9)]);
    flexSheet.applyCellsStyle({
        backgroundColor: '#E1DFDF'
    }, [new wijmo.grid.CellRange(3, 11, 4, 11)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 2));
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        backgroundColor: '#FAD9CD'
    }, [new wijmo.grid.CellRange(9, 1, 9, 11),
        new wijmo.grid.CellRange(17, 1, 17, 11)]);
    flexSheet.applyCellsStyle({
        backgroundColor: '#F4B19B'
    }, [new wijmo.grid.CellRange(10, 1, 16, 11)]);
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(18, 9, 20, 10)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(19, 9, 19, 10));
    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        color: '#808097',
        textAlign: 'center'
    }, [new wijmo.grid.CellRange(20, 1, 20, 1),
        new wijmo.grid.CellRange(20, 5, 20, 5)]);
}

// Generate the formulas sheet.
function generateFormulasSheet(flexSheet) {
    setContentForFormulasSheet(flexSheet);

    applyStyleForFormulasSheet(flexSheet);
}

// Set data for the formulas sheet.
function setContentForFormulasSheet(flexSheet) {
    flexSheet.setCellData(0, 0, "1. Basic Operators");
    flexSheet.setCellData(1, 1, "1.1. Positive/Negative Numbers");
    flexSheet.setCellData(2, 1, "Input a Positive/Negative number.");
    flexSheet.setCellData(3, 1, "Sample:");
    flexSheet.setCellData(3, 2, "-1");
    flexSheet.setCellData(3, 3, "Result:");
    flexSheet.setCellData(3, 4, "=-1");
    flexSheet.setCellData(5, 1, "1.2. Add/Subtract Operators");
    flexSheet.setCellData(6, 1, "Calculates add/sub expression.");
    flexSheet.setCellData(7, 1, "Sample:");
    flexSheet.setCellData(7, 2, "1.25 + 2.17");
    flexSheet.setCellData(7, 3, "Result:");
    flexSheet.setCellData(7, 4, "=1.25 + 2.17");
    flexSheet.setCellData(8, 1, "Sample:");
    flexSheet.setCellData(8, 2, "2.23 - 3.51");
    flexSheet.setCellData(8, 3, "Result:");
    flexSheet.setCellData(8, 4, "=2.23 - 3.51");
    flexSheet.setCellData(10, 1, "1.3. Multiplication/Division Operators");
    flexSheet.setCellData(11, 1, "Calculates mul/div expression.");
    flexSheet.setCellData(12, 1, "Sample:");
    flexSheet.setCellData(12, 2, "12 * 17");
    flexSheet.setCellData(12, 3, "Result:");
    flexSheet.setCellData(12, 4, "=12 * 17");
    flexSheet.setCellData(13, 1, "Sample:");
    flexSheet.setCellData(13, 2, "20 / 6");
    flexSheet.setCellData(13, 3, "Result:");
    flexSheet.setCellData(13, 4, "=20 / 6");
    flexSheet.setCellData(15, 1, "1.4. Power Operator");
    flexSheet.setCellData(16, 1, "Calculates power expression.");
    flexSheet.setCellData(17, 1, "Sample:");
    flexSheet.setCellData(17, 2, "2^3");
    flexSheet.setCellData(17, 3, "Result:");
    flexSheet.setCellData(17, 4, "=2^3");
    flexSheet.setCellData(19, 1, "1.5. Bracket");
    flexSheet.setCellData(20, 1, "Indicates calculation priority by the bracket.");
    flexSheet.setCellData(21, 1, "Sample:");
    flexSheet.setCellData(21, 2, "((1+2)*3)/((4-2)*2)");
    flexSheet.setCellData(22, 1, "Result:");
    flexSheet.setCellData(22, 2, "=((1+2)*3)/((4-2)*2)");
    flexSheet.setCellData(24, 1, "1.6. Percentage");
    flexSheet.setCellData(25, 1, "Parse the percentage to float number.");
    flexSheet.setCellData(26, 1, "Sample:");
    flexSheet.setCellData(26, 2, "23%");
    flexSheet.setCellData(26, 3, "Result:");
    flexSheet.setCellData(26, 4, "=23%");
    flexSheet.setCellData(28, 1, "1.7. Scientific Number");
    flexSheet.setCellData(29, 1, "Parse the scientific number to float number.");
    flexSheet.setCellData(30, 1, "Sample:");
    flexSheet.setCellData(30, 2, "1.2556e2");
    flexSheet.setCellData(30, 3, "Result:");
    flexSheet.setCellData(30, 4, "=1.2556e2");
    flexSheet.setCellData(32, 0, "2. Math function");
    flexSheet.setCellData(33, 1, "2.1. Pi");
    flexSheet.setCellData(34, 1, "Returns the value of pi.");
    flexSheet.setCellData(35, 1, "Sample:");
    flexSheet.setCellData(35, 2, "pi()");
    flexSheet.setCellData(35, 3, "Result:");
    flexSheet.setCellData(35, 4, "=pi()");
    flexSheet.setCellData(37, 1, "2.2. Rand");
    flexSheet.setCellData(38, 1, "Returns a random number between 0 and 1.");
    flexSheet.setCellData(39, 1, "Sample:");
    flexSheet.setCellData(39, 2, "rand()");
    flexSheet.setCellData(39, 3, "Result:");
    flexSheet.setCellData(39, 4, "=rand()");
    flexSheet.setCellData(41, 1, "2.3. Abs");
    flexSheet.setCellData(42, 1, "Returns the absolute value of a number.");
    flexSheet.setCellData(43, 1, "Sample:");
    flexSheet.setCellData(43, 2, "abs(-2.73)");
    flexSheet.setCellData(43, 3, "Result:");
    flexSheet.setCellData(43, 4, "=abs(-2.73)");
    flexSheet.setCellData(45, 1, "2.4. Acos");
    flexSheet.setCellData(46, 1, "Returns the arccosine of a number.");
    flexSheet.setCellData(47, 1, "Sample:");
    flexSheet.setCellData(47, 2, "acos(0.35)");
    flexSheet.setCellData(47, 3, "Result:");
    flexSheet.setCellData(47, 4, "=acos(0.35)");
    flexSheet.setCellData(49, 1, "2.5. Asin");
    flexSheet.setCellData(50, 1, "Returns the arcsine of a number.");
    flexSheet.setCellData(51, 1, "Sample:");
    flexSheet.setCellData(51, 2, "asin(0.5)");
    flexSheet.setCellData(51, 3, "Result:");
    flexSheet.setCellData(51, 4, "=asin(0.5)");
    flexSheet.setCellData(53, 1, "2.6. Atan");
    flexSheet.setCellData(54, 1, "Returns the arctangent of a number.");
    flexSheet.setCellData(55, 1, "Sample:");
    flexSheet.setCellData(55, 2, "atan(0.67)");
    flexSheet.setCellData(55, 3, "Result:");
    flexSheet.setCellData(55, 4, "=atan(0.67)");
    flexSheet.setCellData(57, 1, "2.7. Cos");
    flexSheet.setCellData(58, 1, "Returns the cosine of a number.");
    flexSheet.setCellData(59, 1, "Sample:");
    flexSheet.setCellData(59, 2, "cos(0.6)");
    flexSheet.setCellData(59, 3, "Result:");
    flexSheet.setCellData(59, 4, "=cos(0.6)");
    flexSheet.setCellData(61, 1, "2.8. Sin");
    flexSheet.setCellData(62, 1, "Returns the sine of the given angle.");
    flexSheet.setCellData(63, 1, "Sample:");
    flexSheet.setCellData(63, 2, "sin(0.5)");
    flexSheet.setCellData(63, 3, "Result:");
    flexSheet.setCellData(63, 4, "=sin(0.5)");
    flexSheet.setCellData(65, 1, "2.9. Tan");
    flexSheet.setCellData(66, 1, "Returns the tangent of a number.");
    flexSheet.setCellData(67, 1, "Sample:");
    flexSheet.setCellData(67, 2, "tan(0.75)");
    flexSheet.setCellData(67, 3, "Result:");
    flexSheet.setCellData(67, 4, "=tan(0.75)");
    flexSheet.setCellData(69, 1, "2.10. Atan2");
    flexSheet.setCellData(70, 1, "Returns the arctangent from x- and y-coordinates.");
    flexSheet.setCellData(71, 1, "Sample:");
    flexSheet.setCellData(71, 2, "atan2(90, 15)");
    flexSheet.setCellData(71, 3, "Result:");
    flexSheet.setCellData(71, 4, "=atan2(90, 15)");
    flexSheet.setCellData(73, 1, "2.11. Ceiling");
    flexSheet.setCellData(74, 1, "Rounds a number to the nearest integer or to the nearest multiple of significance.");
    flexSheet.setCellData(75, 1, "Sample:");
    flexSheet.setCellData(75, 2, "ceiling(6.03)");
    flexSheet.setCellData(75, 3, "Result:");
    flexSheet.setCellData(75, 4, "=ceiling(6.03)");
    flexSheet.setCellData(77, 1, "2.12. Floor");
    flexSheet.setCellData(78, 1, "Rounds a number down, toward zero.");
    flexSheet.setCellData(79, 1, "Sample:");
    flexSheet.setCellData(79, 2, "floor(7.96)");
    flexSheet.setCellData(79, 3, "Result:");
    flexSheet.setCellData(79, 4, "=floor(7.96)");
    flexSheet.setCellData(81, 1, "2.13. Round");
    flexSheet.setCellData(82, 1, "Rounds a number to a specified number of digits.");
    flexSheet.setCellData(83, 1, "Sample:");
    flexSheet.setCellData(83, 2, "round(7.56, 1)");
    flexSheet.setCellData(83, 3, "Result:");
    flexSheet.setCellData(83, 4, "=round(7.56, 1)");
    flexSheet.setCellData(84, 1, "Sample:");
    flexSheet.setCellData(84, 2, "round(7.54, 1)");
    flexSheet.setCellData(84, 3, "Result:");
    flexSheet.setCellData(84, 4, "=round(7.54, 1)");
    flexSheet.setCellData(86, 1, "2.14. Exp");
    flexSheet.setCellData(87, 1, "Returns e raised to the power of a given number.");
    flexSheet.setCellData(88, 1, "Sample:");
    flexSheet.setCellData(88, 2, "exp(-1)");
    flexSheet.setCellData(88, 3, "Result:");
    flexSheet.setCellData(88, 4, "=exp(-1)");
    flexSheet.setCellData(90, 1, "2.15. Ln");
    flexSheet.setCellData(91, 1, "Returns the natural logarithm of a number.");
    flexSheet.setCellData(92, 1, "Sample:");
    flexSheet.setCellData(92, 2, "ln(15)");
    flexSheet.setCellData(92, 3, "Result:");
    flexSheet.setCellData(92, 4, "=ln(15)");
    flexSheet.setCellData(94, 1, "2.16. Sqrt");
    flexSheet.setCellData(95, 1, "Returns a positive square root.");
    flexSheet.setCellData(96, 1, "Sample:");
    flexSheet.setCellData(96, 2, "sqrt(16)");
    flexSheet.setCellData(96, 3, "Result:");
    flexSheet.setCellData(96, 4, "=sqrt(16)");
    flexSheet.setCellData(98, 1, "2.17. Power");
    flexSheet.setCellData(99, 1, "Returns the result of a number raised to a power.");
    flexSheet.setCellData(100, 1, "Sample:");
    flexSheet.setCellData(100, 2, "power(1.5, 0.5)");
    flexSheet.setCellData(100, 3, "Result:");
    flexSheet.setCellData(100, 4, "=power(1.5, 0.5)");
    flexSheet.setCellData(102, 1, "2.18. Mod");
    flexSheet.setCellData(103, 1, "Returns the remainder from division.");
    flexSheet.setCellData(104, 1, "Sample:");
    flexSheet.setCellData(104, 2, "mod(11, 3)");
    flexSheet.setCellData(104, 3, "Result:");
    flexSheet.setCellData(104, 4, "=mod(11, 3)");
    flexSheet.setCellData(106, 1, "2.19. Rounddown");
    flexSheet.setCellData(107, 1, "Rounds a number down, toward zero.");
    flexSheet.setCellData(108, 1, "Sample:");
    flexSheet.setCellData(108, 2, "rounddown(11.987, 2)");
    flexSheet.setCellData(108, 4, "Result:");
    flexSheet.setCellData(108, 5, "=rounddown(11.987, 2)");
    flexSheet.setCellData(110, 1, "2.20. Roundup");
    flexSheet.setCellData(111, 1, "Rounds a number up, away from zero.");
    flexSheet.setCellData(112, 1, "Sample:");
    flexSheet.setCellData(112, 2, "roundup(11.982, 2)");
    flexSheet.setCellData(112, 4, "Result:");
    flexSheet.setCellData(112, 5, "=roundup(11.982, 2)");
    flexSheet.setCellData(114, 1, "2.21. Trunc");
    flexSheet.setCellData(115, 1, "Truncates a number to an integer.");
    flexSheet.setCellData(116, 1, "Sample:");
    flexSheet.setCellData(116, 2, "trunc(8.9)");
    flexSheet.setCellData(116, 3, "Result:");
    flexSheet.setCellData(116, 4, "=trunc(8.9)");
    flexSheet.setCellData(118, 0, "3. Logical function");
    flexSheet.setCellData(119, 1, "3.1. Compare operators");
    flexSheet.setCellData(120, 1, "Gets boolean result of the compare operators such as (>, <, >=, <=, =, <>).");
    flexSheet.setCellData(121, 1, "Sample:");
    flexSheet.setCellData(121, 2, "1>2");
    flexSheet.setCellData(121, 3, "Result:");
    flexSheet.setCellData(121, 4, "=1>2");
    flexSheet.setCellData(123, 1, "3.2. True");
    flexSheet.setCellData(124, 1, "Returns the logical value TRUE.");
    flexSheet.setCellData(125, 1, "Sample:");
    flexSheet.setCellData(125, 2, "true()");
    flexSheet.setCellData(125, 3, "Result:");
    flexSheet.setCellData(125, 4, "=true()");
    flexSheet.setCellData(127, 1, "3.3. False");
    flexSheet.setCellData(128, 1, "Returns the logical value FALSE.");
    flexSheet.setCellData(129, 1, "Sample:");
    flexSheet.setCellData(129, 2, "false()");
    flexSheet.setCellData(129, 3, "Result:");
    flexSheet.setCellData(129, 4, "=false()");
    flexSheet.setCellData(131, 1, "3.4. And");
    flexSheet.setCellData(132, 1, "Returns TRUE if all of its arguments are TRUE.");
    flexSheet.setCellData(133, 1, "Sample:");
    flexSheet.setCellData(133, 2, "and(true(),1>2)");
    flexSheet.setCellData(133, 3, "Result:");
    flexSheet.setCellData(133, 4, "=and(true(),1>2)");
    flexSheet.setCellData(135, 1, "3.5. Or");
    flexSheet.setCellData(136, 1, "Returns TRUE if any argument is TRUE.");
    flexSheet.setCellData(137, 1, "Sample:");
    flexSheet.setCellData(137, 2, "or(false(),1<2)");
    flexSheet.setCellData(137, 3, "Result:");
    flexSheet.setCellData(137, 4, "=or(false(),1<2)");
    flexSheet.setCellData(139, 1, "3.6. Not");
    flexSheet.setCellData(140, 1, "Reverses the logic of its argument.");
    flexSheet.setCellData(141, 1, "Sample:");
    flexSheet.setCellData(141, 2, "not(1<2)");
    flexSheet.setCellData(141, 3, "Result:");
    flexSheet.setCellData(141, 4, "=not(1<2)");
    flexSheet.setCellData(143, 1, "3.7. If");
    flexSheet.setCellData(144, 1, "Specifies a logical test to perform.");
    flexSheet.setCellData(145, 1, "Sample:");
    flexSheet.setCellData(145, 2, "if(true(), \"true result\", \"false result\")");
    flexSheet.setCellData(146, 1, "Result:");
    flexSheet.setCellData(146, 2, "=if(true(), \"true result\", \"false result\")");
    flexSheet.setCellData(148, 0, "4. Text process function");
    flexSheet.setCellData(149, 1, "4.1. Char");
    flexSheet.setCellData(150, 1, "Returns the character specified by the code number.");
    flexSheet.setCellData(151, 1, "Sample:");
    flexSheet.setCellData(151, 2, "char(65)");
    flexSheet.setCellData(151, 3, "Result:");
    flexSheet.setCellData(151, 4, "=char(65)");
    flexSheet.setCellData(153, 1, "4.2. Code");
    flexSheet.setCellData(154, 1, "Returns a numeric code for the first character in a text string.");
    flexSheet.setCellData(155, 1, "Sample:");
    flexSheet.setCellData(155, 2, "code(\"a\")");
    flexSheet.setCellData(155, 3, "Result:");
    flexSheet.setCellData(155, 4, "=code(\"a\")");
    flexSheet.setCellData(157, 1, "4.3. Concatenate");
    flexSheet.setCellData(158, 1, "Joins several text items into one text item.");
    flexSheet.setCellData(159, 1, "Sample:");
    flexSheet.setCellData(159, 2, "concatenate(\"Hello \", \"World!\")");
    flexSheet.setCellData(160, 1, "Result:");
    flexSheet.setCellData(160, 2, "=concatenate(\"Hello \", \"World!\")");
    flexSheet.setCellData(162, 1, "4.4. Left");
    flexSheet.setCellData(163, 1, "Returns the leftmost characters from a text value.");
    flexSheet.setCellData(164, 1, "Sample:");
    flexSheet.setCellData(164, 2, "left(\"Abcdef\",3)");
    flexSheet.setCellData(164, 3, "Result:");
    flexSheet.setCellData(164, 4, "=left(\"Abcdef\",3)");
    flexSheet.setCellData(166, 1, "4.5. Right");
    flexSheet.setCellData(167, 1, "Returns the rightmost characters from a text value.");
    flexSheet.setCellData(168, 1, "Sample:");
    flexSheet.setCellData(168, 2, "right(\"Abcdef\",3)");
    flexSheet.setCellData(168, 3, "Result:");
    flexSheet.setCellData(168, 4, "=right(\"Abcdef\",3)");
    flexSheet.setCellData(170, 1, "4.6. Mid");
    flexSheet.setCellData(171, 1, "Returns a specific number of characters from a text string starting at the position you specify.");
    flexSheet.setCellData(172, 1, "Sample:");
    flexSheet.setCellData(172, 2, "mid(\"Abcdef\",3,2)");
    flexSheet.setCellData(173, 1, "Result:");
    flexSheet.setCellData(173, 2, "=mid(\"Abcdef\",3,2)");
    flexSheet.setCellData(175, 1, "4.7. Len");
    flexSheet.setCellData(176, 1, "Returns the number of characters in a text string.");
    flexSheet.setCellData(177, 1, "Sample:");
    flexSheet.setCellData(177, 2, "len(\"Abcdef\")");
    flexSheet.setCellData(177, 3, "Result:");
    flexSheet.setCellData(177, 4, "=len(\"Abcdef\")");
    flexSheet.setCellData(179, 1, "4.8. Find");
    flexSheet.setCellData(180, 1, "Finds one text value within another (case-sensitive).");
    flexSheet.setCellData(181, 1, "Sample:");
    flexSheet.setCellData(181, 2, "find(\"Bc\",\"ABcdef\")");
    flexSheet.setCellData(182, 1, "Result:");
    flexSheet.setCellData(182, 2, "=find(\"Bc\",\"ABcdef\")");
    flexSheet.setCellData(184, 1, "4.9. Search");
    flexSheet.setCellData(185, 1, "Finds one text value within another (not case-sensitive).");
    flexSheet.setCellData(186, 1, "Sample:");
    flexSheet.setCellData(186, 2, "search(\"bc\",\"ABcdef\")");
    flexSheet.setCellData(187, 1, "Result:");
    flexSheet.setCellData(187, 2, "=search(\"bc\",\"ABcdef\")");
    flexSheet.setCellData(189, 1, "4.10. Lower");
    flexSheet.setCellData(190, 1, "Converts text to lowercase.");
    flexSheet.setCellData(191, 1, "Sample:");
    flexSheet.setCellData(191, 2, "lower(\"ABCDE\")");
    flexSheet.setCellData(191, 3, "Result:");
    flexSheet.setCellData(191, 4, "=lower(\"ABCDE\")");
    flexSheet.setCellData(193, 1, "4.11. Upper");
    flexSheet.setCellData(194, 1, "Converts text to uppercase.");
    flexSheet.setCellData(195, 1, "Sample:");
    flexSheet.setCellData(195, 2, "upper(\"abcdef\")");
    flexSheet.setCellData(195, 3, "Result:");
    flexSheet.setCellData(195, 4, "=upper(\"abcdef\")");
    flexSheet.setCellData(197, 1, "4.12. Proper");
    flexSheet.setCellData(198, 1, "Capitalizes the first letter in each word of a text value.");
    flexSheet.setCellData(199, 1, "Sample:");
    flexSheet.setCellData(199, 2, "proper(\"abcde\")");
    flexSheet.setCellData(199, 3, "Result:");
    flexSheet.setCellData(199, 4, "=proper(\"abcde\")");
    flexSheet.setCellData(201, 1, "4.13. Trim");
    flexSheet.setCellData(202, 1, "Removes spaces from text.");
    flexSheet.setCellData(203, 1, "Sample:");
    flexSheet.setCellData(203, 2, "trim(\"   abcde   \")");
    flexSheet.setCellData(203, 3, "Result:");
    flexSheet.setCellData(203, 4, "=trim(\"   abcde   \")");
    flexSheet.setCellData(205, 1, "4.14. Replace");
    flexSheet.setCellData(206, 1, "Replaces characters within text.");
    flexSheet.setCellData(207, 1, "Sample:");
    flexSheet.setCellData(207, 2, "replace(\"abcdefg\",2,3,\"wxyz\")");
    flexSheet.setCellData(208, 1, "Result:");
    flexSheet.setCellData(208, 2, "=replace(\"abcdefg\",2,3,\"wxyz\")");
    flexSheet.setCellData(210, 1, "4.15. Substitute");
    flexSheet.setCellData(211, 1, "Substitutes new text for old text in a text string.");
    flexSheet.setCellData(212, 1, "Sample:");
    flexSheet.setCellData(212, 2, "substitute(\"abcabcdabcdef\",\"ab\",\"xy\")");
    flexSheet.setCellData(213, 1, "Result:");
    flexSheet.setCellData(213, 2, "=substitute(\"abcabcdabcdef\",\"ab\",\"xy\")");
    flexSheet.setCellData(215, 1, "4.16. Rept");
    flexSheet.setCellData(216, 1, "Repeats text a given number of times.");
    flexSheet.setCellData(217, 1, "Sample:");
    flexSheet.setCellData(217, 2, "rept(\"abc\",3)");
    flexSheet.setCellData(217, 3, "Result:");
    flexSheet.setCellData(217, 4, "=rept(\"abc\",3)");
    flexSheet.setCellData(219, 1, "4.17. Text");
    flexSheet.setCellData(220, 1, "Formats a number and converts it to text.");
    flexSheet.setCellData(221, 1, "Sample:");
    flexSheet.setCellData(221, 2, "text(1234,\"c2\")");
    flexSheet.setCellData(221, 3, "Result:");
    flexSheet.setCellData(221, 4, "=text(1234,\"c2\")");
    flexSheet.setCellData(223, 1, "4.18. Value");
    flexSheet.setCellData(224, 1, "Converts a text argument to a number.");
    flexSheet.setCellData(225, 1, "Sample:");
    flexSheet.setCellData(225, 2, "value(\"1234\")");
    flexSheet.setCellData(225, 3, "Result:");
    flexSheet.setCellData(225, 4, "=value(\"1234\")");
    flexSheet.setCellData(227, 0, "5. Aggregate function");
    flexSheet.setCellData(228, 1, "sample data:");
    for (var rowIndex = 229; rowIndex <= 232; rowIndex++) {
        for (var colIndex = 1; colIndex <= 8; colIndex++) {
            flexSheet.setCellData(rowIndex, colIndex, Math.random() * 200);
        }
    }
    flexSheet.setCellData(234, 1, "5.1. Sum");
    flexSheet.setCellData(235, 1, "Adds its arguments.");
    flexSheet.setCellData(236, 1, "Sample:");
    flexSheet.setCellData(236, 2, "sum(B230:D232)");
    flexSheet.setCellData(237, 1, "Result:");
    flexSheet.setCellData(237, 2, "=sum(B230:D232)");
    flexSheet.setCellData(238, 1, "Sample:");
    flexSheet.setCellData(238, 2, "sum(1,3,5,7,10,12,13)");
    flexSheet.setCellData(239, 1, "Result:");
    flexSheet.setCellData(239, 2, "=sum(1,3,5,7,10,12,13)");
    flexSheet.setCellData(241, 1, "5.2. Average");
    flexSheet.setCellData(242, 1, "Returns the average of its arguments.");
    flexSheet.setCellData(243, 1, "Sample:");
    flexSheet.setCellData(243, 2, "average(C230:E231)");
    flexSheet.setCellData(244, 1, "Result:");
    flexSheet.setCellData(244, 2, "=average(C230:E231)");
    flexSheet.setCellData(245, 1, "Sample:");
    flexSheet.setCellData(245, 2, "average(2,4,5,7,11,13,19)");
    flexSheet.setCellData(246, 1, "Result:");
    flexSheet.setCellData(246, 2, "=average(2,4,5,7,11,13,19)");
    flexSheet.setCellData(248, 1, "5.3. Count");
    flexSheet.setCellData(249, 1, "Counts how many numbers are in the list of arguments.");
    flexSheet.setCellData(250, 1, "Sample:");
    flexSheet.setCellData(250, 2, "count(B231:E233)");
    flexSheet.setCellData(251, 1, "Result:");
    flexSheet.setCellData(251, 2, "=count(B231:E233)");
    flexSheet.setCellData(252, 1, "Sample:");
    flexSheet.setCellData(252, 2, "count(1,7,8,10,11,16,19)");
    flexSheet.setCellData(253, 1, "Result:");
    flexSheet.setCellData(253, 2, "=count(1,7,8,10,11,16,19)");
    flexSheet.setCellData(255, 1, "5.4. Max");
    flexSheet.setCellData(256, 1, "Returns the maximum value in a list of arguments.");
    flexSheet.setCellData(257, 1, "Sample:");
    flexSheet.setCellData(257, 2, "max(C231:F233)");
    flexSheet.setCellData(258, 1, "Result:");
    flexSheet.setCellData(258, 2, "=max(C231:F233)");
    flexSheet.setCellData(259, 1, "Sample:");
    flexSheet.setCellData(259, 2, "max(100,87,103,54,75,34)");
    flexSheet.setCellData(260, 1, "Result:");
    flexSheet.setCellData(260, 2, "=max(100,87,103,54,75,34)");
    flexSheet.setCellData(262, 1, "5.5. Min");
    flexSheet.setCellData(263, 1, "Returns the minimum value in a list of arguments.");
    flexSheet.setCellData(264, 1, "Sample:");
    flexSheet.setCellData(264, 2, "min(B230:G233)");
    flexSheet.setCellData(265, 1, "Result:");
    flexSheet.setCellData(265, 2, "=min(B230:G233)");
    flexSheet.setCellData(266, 1, "Sample:");
    flexSheet.setCellData(266, 2, "min(74,47,68,99,106,13,51)");
    flexSheet.setCellData(267, 1, "Result:");
    flexSheet.setCellData(267, 2, "=min(74,47,68,99,106,13,51)");
    flexSheet.setCellData(269, 1, "5.6. StDev");
    flexSheet.setCellData(270, 1, "Estimates standard deviation based on a sample.");
    flexSheet.setCellData(271, 1, "Sample:");
    flexSheet.setCellData(271, 2, "stdev(B231:G233)");
    flexSheet.setCellData(272, 1, "Result:");
    flexSheet.setCellData(272, 2, "=stdev(B231:G233)");
    flexSheet.setCellData(273, 1, "Sample:");
    flexSheet.setCellData(273, 2, "stdev(74,47,68,99,106,13,51)");
    flexSheet.setCellData(274, 1, "Result:");
    flexSheet.setCellData(274, 2, "=stdev(74,47,68,99,106,13,51)");
    flexSheet.setCellData(276, 1, "5.7. StDevP");
    flexSheet.setCellData(277, 1, "Calculates standard deviation based on the entire population.");
    flexSheet.setCellData(278, 1, "Sample:");
    flexSheet.setCellData(278, 2, "stdevp(B231:G233)");
    flexSheet.setCellData(279, 1, "Result:");
    flexSheet.setCellData(279, 2, "=stdevp(B231:G233)");
    flexSheet.setCellData(280, 1, "Sample:");
    flexSheet.setCellData(280, 2, "stdevp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(281, 1, "Result:");
    flexSheet.setCellData(281, 2, "=stdevp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(283, 1, "5.8. Var");
    flexSheet.setCellData(284, 1, "Estimates variance based on a sample.");
    flexSheet.setCellData(285, 1, "Sample:");
    flexSheet.setCellData(285, 2, "var(C230:H232)");
    flexSheet.setCellData(286, 1, "Result:");
    flexSheet.setCellData(286, 2, "=var(C230:H232)");
    flexSheet.setCellData(287, 1, "Sample:");
    flexSheet.setCellData(287, 2, "var(74,47,68,99,106,13,51)");
    flexSheet.setCellData(288, 1, "Result:");
    flexSheet.setCellData(288, 2, "=var(74,47,68,99,106,13,51)");
    flexSheet.setCellData(290, 1, "5.9. VarP");
    flexSheet.setCellData(291, 1, "Calculates variance based on the entire population.");
    flexSheet.setCellData(292, 1, "Sample:");
    flexSheet.setCellData(292, 2, "varp(C230:H232)");
    flexSheet.setCellData(293, 1, "Result:");
    flexSheet.setCellData(293, 2, "=varp(C230:H232)");
    flexSheet.setCellData(294, 1, "Sample:");
    flexSheet.setCellData(294, 2, "varp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(295, 1, "Result:");
    flexSheet.setCellData(295, 2, "=varp(74,47,68,99,106,13,51)");
    flexSheet.setCellData(297, 0, "6. Date function");
    flexSheet.setCellData(298, 1, "6.1. Now");
    flexSheet.setCellData(299, 1, "Returns the serial number of the current date and time.");
    flexSheet.setCellData(300, 1, "Sample:");
    flexSheet.setCellData(300, 2, "Now()");
    flexSheet.setCellData(300, 3, "Result:");
    flexSheet.setCellData(300, 4, "=Now()");
    flexSheet.setCellData(302, 1, "6.2. Year");
    flexSheet.setCellData(303, 1, "Converts a serial number to a year.");
    flexSheet.setCellData(304, 1, "Sample:");
    flexSheet.setCellData(304, 2, "Year(E301)");
    flexSheet.setCellData(304, 3, "Result:");
    flexSheet.setCellData(304, 4, "=Year(E301)");
    flexSheet.setCellData(306, 1, "6.3. Month");
    flexSheet.setCellData(307, 1, "Converts a serial number to a month.");
    flexSheet.setCellData(308, 1, "Sample:");
    flexSheet.setCellData(308, 2, "Month(E301)");
    flexSheet.setCellData(308, 3, "Result:");
    flexSheet.setCellData(308, 4, "=Month(E301)");
    flexSheet.setCellData(310, 1, "6.4. Day");
    flexSheet.setCellData(311, 1, "Converts a serial number to a day of the month.");
    flexSheet.setCellData(312, 1, "Sample:");
    flexSheet.setCellData(312, 2, "Day(E301)");
    flexSheet.setCellData(312, 3, "Result:");
    flexSheet.setCellData(312, 4, "=Day(E301)");
    flexSheet.setCellData(314, 0, "7. Lookup & Reference");
    flexSheet.setCellData(315, 1, "7.1. Cell Reference");
    flexSheet.setCellData(316, 1, "Gets value for a specific cell in the flexsheet.");
    flexSheet.setCellData(317, 1, "Sample:");
    flexSheet.setCellData(317, 2, "B317");
    flexSheet.setCellData(317, 3, "Result:");
    flexSheet.setCellData(317, 4, "=B317");
    flexSheet.setCellData(319, 1, "7.2. Choose");
    flexSheet.setCellData(320, 1, "Chooses a value from a list of values.");
    flexSheet.setCellData(321, 1, "Sample:");
    flexSheet.setCellData(321, 2, "choose(2, \"Hello\", \"World\", \"for\", \"test\")");
    flexSheet.setCellData(322, 1, "Result:");
    flexSheet.setCellData(322, 2, "=choose(2, \"Hello\", \"World\", \"for\", \"test\")");
    flexSheet.setCellData(324, 1, "7.3. Column");
    flexSheet.setCellData(325, 1, "Returns the column number of a reference.");
    flexSheet.setCellData(326, 1, "Sample:");
    flexSheet.setCellData(326, 2, "column(E1)");
    flexSheet.setCellData(326, 3, "Result:");
    flexSheet.setCellData(326, 4, "=column(E1)");
    flexSheet.setCellData(328, 1, "7.4. Columns");
    flexSheet.setCellData(329, 1, "Returns the number of columns in a reference.");
    flexSheet.setCellData(330, 1, "Sample:");
    flexSheet.setCellData(330, 2, "columns(B2:D5)");
    flexSheet.setCellData(330, 3, "Result:");
    flexSheet.setCellData(330, 4, "=columns(B2:D5)");
    flexSheet.setCellData(332, 1, "7.5. Row");
    flexSheet.setCellData(333, 1, "Returns the row number of a reference.");
    flexSheet.setCellData(334, 1, "Sample:");
    flexSheet.setCellData(334, 2, "row(B21)");
    flexSheet.setCellData(334, 3, "Result:");
    flexSheet.setCellData(334, 4, "=row(B21)");
    flexSheet.setCellData(336, 1, "7.6. Rows");
    flexSheet.setCellData(337, 1, "Returns the number of rows in a reference.");
    flexSheet.setCellData(338, 1, "Sample:");
    flexSheet.setCellData(338, 2, "rows(B21:E13)");
    flexSheet.setCellData(338, 3, "Result:");
    flexSheet.setCellData(338, 4, "=rows(B21:E13)");
    flexSheet.setCellData(341, 0, "2016v1 added formulas");
    flexSheet.setCellData(342, 0, "1. Date function");
    flexSheet.setCellData(343, 1, "1.1. Today");
    flexSheet.setCellData(344, 1, "Returns the serial number of today's date.");
    flexSheet.setCellData(345, 1, "Sample:");
    flexSheet.setCellData(345, 2, "today()");
    flexSheet.setCellData(345, 3, "Result:");
    flexSheet.setCellData(345, 4, "=today()");
    flexSheet.setCellData(347, 1, "1.2. Date");
    flexSheet.setCellData(348, 1, "Returns the serial number of a particular date.");
    flexSheet.setCellData(349, 1, "Sample:");
    flexSheet.setCellData(349, 2, "date(2015, 11, 26)");
    flexSheet.setCellData(350, 1, "Result:");
    flexSheet.setCellData(350, 2, "=date(2015, 11, 26)");
    flexSheet.setCellData(352, 1, "1.3. Time");
    flexSheet.setCellData(353, 1, "Returns the serial number of a particular time.");
    flexSheet.setCellData(354, 1, "Sample:");
    flexSheet.setCellData(354, 2, "time(11, 28, 33)");
    flexSheet.setCellData(355, 1, "Result:");
    flexSheet.setCellData(355, 2, "=time(11, 28, 33)");
    flexSheet.setCellData(357, 1, "1.4. Hour");
    flexSheet.setCellData(358, 1, "Converts a serial number to an hour.");
    flexSheet.setCellData(359, 1, "Sample:");
    flexSheet.setCellData(359, 2, "hour(C356)");
    flexSheet.setCellData(359, 3, "Result:");
    flexSheet.setCellData(359, 4, "=hour(C356)");
    flexSheet.setCellData(360, 1, "Sample:");
    flexSheet.setCellData(360, 2, "hour(0.65)");
    flexSheet.setCellData(360, 3, "Result:");
    flexSheet.setCellData(360, 4, "=hour(0.65)");
    flexSheet.setCellData(362, 1, "1.5. DateDif");
    flexSheet.setCellData(363, 1, "Calculates the number of days, months, or years between two dates.");
    flexSheet.setCellData(364, 1, "Syntax:");
    flexSheet.setCellData(364, 2, "DateDif(start_date, end_date, unit)");
    flexSheet.setCellData(365, 1, "The unit paratemer can be following values:");
    flexSheet.setCellData(366, 1, "\"Y\"");
    flexSheet.setCellData(366, 2, "The number of complete years in the period.");
    flexSheet.setCellData(367, 1, "\"M\"");
    flexSheet.setCellData(367, 2, "The number of complete months in the period.");
    flexSheet.setCellData(368, 1, "\"D\"");
    flexSheet.setCellData(368, 2, "The number of days in the period.");
    flexSheet.setCellData(369, 1, "\"MD\"");
    flexSheet.setCellData(369, 2, "The difference between the days in start_date and end_date. The months and years of the dates are ignored.");
    flexSheet.setCellData(370, 1, "\"YM\"");
    flexSheet.setCellData(370, 2, "The difference between the months in start_date and end_date. The days and years of the dates are ignored.");
    flexSheet.setCellData(371, 1, "\"YD\"");
    flexSheet.setCellData(371, 2, "The difference between the days of start_date and end_date. The years of the dates are ignored.");
    flexSheet.setCellData(372, 1, "Sample:");
    flexSheet.setCellData(372, 2, "datedif(\"11/26/2012\", \"8/15/2015\", \"Y\")");
    flexSheet.setCellData(373, 1, "Result:");
    flexSheet.setCellData(373, 2, "=datedif(\"11/26/2012\", \"8/15/2015\", \"Y\")");
    flexSheet.setCellData(374, 1, "Sample:");
    flexSheet.setCellData(374, 2, "datedif(\"5/26/2015\", \"11/15/2015\", \"M\")");
    flexSheet.setCellData(375, 1, "Result:");
    flexSheet.setCellData(375, 2, "=datedif(\"5/26/2015\", \"11/15/2015\", \"M\")");
    flexSheet.setCellData(376, 1, "Sample:");
    flexSheet.setCellData(376, 2, "datedif(\"2/26/2014\", \"3/15/2015\", \"D\")");
    flexSheet.setCellData(377, 1, "Result:");
    flexSheet.setCellData(377, 2, "=datedif(\"2/26/2014\", \"3/15/2015\", \"D\")");
    flexSheet.setCellData(378, 1, "Sample:");
    flexSheet.setCellData(378, 2, "datedif(\"3/26/2015\", \"2/15/2016\", \"MD\")");
    flexSheet.setCellData(379, 1, "Result:");
    flexSheet.setCellData(379, 2, "=datedif(\"3/26/2015\", \"2/15/2016\", \"MD\")");
    flexSheet.setCellData(380, 1, "Sample:");
    flexSheet.setCellData(380, 2, "datedif(\"11/26/2015\", \"2/15/2016\", \"YM\")");
    flexSheet.setCellData(381, 1, "Result:");
    flexSheet.setCellData(381, 2, "=datedif(\"11/26/2015\", \"2/15/2016\", \"YM\")");
    flexSheet.setCellData(382, 1, "Sample:");
    flexSheet.setCellData(382, 2, "datedif(\"2/26/2016\", \"2/15/2017\", \"YD\")");
    flexSheet.setCellData(383, 1, "Result:");
    flexSheet.setCellData(383, 2, "=datedif(\"2/26/2016\", \"2/15/2017\", \"YD\")");
    flexSheet.setCellData(385, 0, "2. Aggregate function");
    flexSheet.setCellData(386, 1, "sample data:");
    flexSheet.setCellData(387, 1, "Tree");
    flexSheet.setCellData(387, 2, "Height");
    flexSheet.setCellData(387, 3, "Age");
    flexSheet.setCellData(387, 4, "Yield");
    flexSheet.setCellData(387, 5, "Profit");
    flexSheet.setCellData(387, 6, "Height");
    flexSheet.setCellData(388, 1, "Apple");
    flexSheet.setCellData(388, 2, ">10");
    flexSheet.setCellData(388, 6, "<16");
    flexSheet.setCellData(389, 1, "Pear");
    flexSheet.setCellData(391, 1, "Tree");
    flexSheet.setCellData(391, 2, "Height");
    flexSheet.setCellData(391, 3, "Age");
    flexSheet.setCellData(391, 4, "Yield");
    flexSheet.setCellData(391, 5, "Profit");
    for (var rowIndex = 392; rowIndex <= 401; rowIndex++) {
        for (var colIndex = 1; colIndex <= 5; colIndex++) {
            if (colIndex === 1) {
                if (rowIndex === 394) {
                    flexSheet.setCellData(rowIndex, colIndex, "Apple");
                } else {
                    flexSheet.setCellData(rowIndex, colIndex, ["Apple", "Pear", "Cherry", "Orange"][Math.floor(Math.random() * 4)]);
                }
            } else if (colIndex === 5) {
                flexSheet.setCellData(rowIndex, colIndex, Math.random() * 300);
            } else {
                if (rowIndex === 394 && colIndex === 2) {
                    flexSheet.setCellData(rowIndex, colIndex, 15);
                } else if (rowIndex === 394 && colIndex === 3) {
                    flexSheet.setCellData(rowIndex, colIndex, "N/A");
                } else {
                    flexSheet.setCellData(rowIndex, colIndex, Math.round(Math.random() * 20));
                }
            }
        }
    }
    flexSheet.setCellData(403, 1, "2.1. CountA");
    flexSheet.setCellData(404, 1, "Counts how many values are in the list of arguments.");
    flexSheet.setCellData(405, 1, "Sample:");
    flexSheet.setCellData(405, 2, "counta(E388:E402)");
    flexSheet.setCellData(406, 1, "Result:");
    flexSheet.setCellData(406, 2, "=counta(E388:E402)");
    flexSheet.setCellData(408, 1, "2.2. CountBlank");
    flexSheet.setCellData(409, 1, "Counts the number of blank cells within a range.");
    flexSheet.setCellData(410, 1, "Sample:");
    flexSheet.setCellData(410, 2, "countblank(E388:E402)");
    flexSheet.setCellData(411, 1, "Result:");
    flexSheet.setCellData(411, 2, "=countblank(E388:E402)");
    flexSheet.setCellData(413, 1, "2.3. CountIf");
    flexSheet.setCellData(414, 1, "Counts the number of cells within a range that meet the given criteria.");
    flexSheet.setCellData(415, 1, "Syntax:");
    flexSheet.setCellData(415, 2, "countif(range, criteria)");
    flexSheet.setCellData(416, 1, "Sample:");
    flexSheet.setCellData(416, 2, "countif(B393:B402, \"Apple\")");
    flexSheet.setCellData(417, 1, "Result:");
    flexSheet.setCellData(417, 2, "=countif(B393:B402, \"Apple\")");
    flexSheet.setCellData(418, 1, "Sample:");
    flexSheet.setCellData(418, 2, "countif(C393:C402, \">10\")");
    flexSheet.setCellData(419, 1, "Result:");
    flexSheet.setCellData(419, 2, "=countif(C393:C402, \">10\")");
    flexSheet.setCellData(421, 1, "2.4. CountIfs");
    flexSheet.setCellData(422, 1, "Counts the number of cells within a range that meet multiple criteria.");
    flexSheet.setCellData(423, 1, "Syntax:");
    flexSheet.setCellData(423, 2, "countifs(criteria_range1, criteria1, [criteria_range2, criteria2],...)");
    flexSheet.setCellData(424, 1, "Sample:");
    flexSheet.setCellData(424, 2, "countifs(B393:B402, \"Apple\", C393:C402, \">10\")");
    flexSheet.setCellData(425, 1, "Result:");
    flexSheet.setCellData(425, 2, "=countifs(B393:B402, \"Apple\", C393:C402, \">10\")");
    flexSheet.setCellData(427, 1, "2.5. DCount");
    flexSheet.setCellData(428, 1, "Counts the cells that contain numbers in a database.");
    flexSheet.setCellData(429, 1, "Syntax:");
    flexSheet.setCellData(429, 2, "countifs(count_range, field, criteria_range)");
    flexSheet.setCellData(430, 1, "Sample:");
    flexSheet.setCellData(430, 2, "dcount(B392:F402, \"Age\", B388:G390)");
    flexSheet.setCellData(431, 1, "Result:");
    flexSheet.setCellData(431, 2, "=dcount(B392:F402, \"Age\", B388:G390)");
    flexSheet.setCellData(433, 1, "2.6. SumIf");
    flexSheet.setCellData(434, 1, "Adds the cells specified by a given criteria.");
    flexSheet.setCellData(435, 1, "Syntax:");
    flexSheet.setCellData(435, 2, "sumif(range, criteria, [sum_range])");
    flexSheet.setCellData(436, 1, "Remarks:");
    flexSheet.setCellData(436, 2, "If the sum_range argument is omitted, FlexSheet adds the cells that are specified in the range argument.");
    flexSheet.setCellData(437, 1, "Sample:");
    flexSheet.setCellData(437, 2, "sumif(B393:B402, \"Apple\", C393:C402)");
    flexSheet.setCellData(438, 1, "Result:");
    flexSheet.setCellData(438, 2, "=sumif(B393:B402, \"Apple\", C393:C402)");
    flexSheet.setCellData(439, 1, "Sample:");
    flexSheet.setCellData(439, 2, "sumif(C393:C402, \">10\")");
    flexSheet.setCellData(440, 1, "Result:");
    flexSheet.setCellData(440, 2, "=sumif(C393:C402, \">10\")");
    flexSheet.setCellData(442, 1, "2.7. SumIfs");
    flexSheet.setCellData(443, 1, "Adds the cells in a range that meet multiple criteria.");
    flexSheet.setCellData(444, 1, "Syntax:");
    flexSheet.setCellData(444, 2, "sumifs(sum_range, criteria_range1, criteria1, [criteria_range2, criteria2],...)");
    flexSheet.setCellData(445, 1, "Sample:");
    flexSheet.setCellData(445, 2, "sumifs(F393:F402, B393:B402, \"Apple\", C393:C402, \">10\")");
    flexSheet.setCellData(446, 1, "Result:");
    flexSheet.setCellData(446, 2, "=sumifs(F393:F402, B393:B402, \"Apple\", C393:C402, \">10\")");
    flexSheet.setCellData(448, 1, "2.8. Rank");
    flexSheet.setCellData(449, 1, "Returns the rank of a number in a list of numbers.");
    flexSheet.setCellData(450, 1, "Syntax:");
    flexSheet.setCellData(450, 2, "rank(number, ref, [order])");
    flexSheet.setCellData(451, 1, "Remarks:");
    flexSheet.setCellData(451, 2, "If order is 0 (zero) or omitted, FlexSheet ranks number as if ref were a list sorted in descending order.");
    flexSheet.setCellData(452, 2, "If order is any nonzero value, FlexSheet ranks number as if ref were a list sorted in ascending order.");
    flexSheet.setCellData(453, 1, "Sample:");
    flexSheet.setCellData(453, 2, "rank(15, C393:C402)");
    flexSheet.setCellData(454, 1, "Result:");
    flexSheet.setCellData(454, 2, "=rank(15, C393:C402)");
    flexSheet.setCellData(455, 1, "Sample:");
    flexSheet.setCellData(455, 2, "rank(15, C393:C402, 1)");
    flexSheet.setCellData(456, 1, "Result:");
    flexSheet.setCellData(456, 2, "=rank(15, C393:C402, 1)");
    flexSheet.setCellData(458, 1, "2.9. Product");
    flexSheet.setCellData(459, 1, "Multiplies its arguments.");
    flexSheet.setCellData(460, 1, "Sample:");
    flexSheet.setCellData(460, 2, "product(C393:E393)");
    flexSheet.setCellData(461, 1, "Result:");
    flexSheet.setCellData(461, 2, "=product(C393:E393)");
    flexSheet.setCellData(462, 1, "Sample:");
    flexSheet.setCellData(462, 2, "product(1, 2, 3, 4, 5)");
    flexSheet.setCellData(463, 1, "Result:");
    flexSheet.setCellData(463, 2, "=product(1, 2, 3, 4, 5)");
    flexSheet.setCellData(465, 1, "2.10. Subtotal");
    flexSheet.setCellData(466, 1, "Returns a subtotal in a list or database.");
    flexSheet.setCellData(467, 1, "Syntax:");
    flexSheet.setCellData(467, 2, "subtotal(function_num, ref1, [ref2],...)");
    flexSheet.setCellData(468, 1, "Remarks:");
    flexSheet.setCellData(468, 2, "The function_num 1-11 or 101-111 that specifies the function to use for the subtotal.");
    flexSheet.setCellData(469, 2, "1-11 includes manually-hidden rows, while 101-111 excludes them.");
    flexSheet.setCellData(470, 2, "Function_Num");
    flexSheet.setCellData(470, 4, "Function_Num");
    flexSheet.setCellData(470, 6, "Function");
    flexSheet.setCellData(471, 2, "(includes hidden values)");
    flexSheet.setCellData(471, 4, "(ignores hidden values)");
    flexSheet.setCellData(472, 2, "1");
    flexSheet.setCellData(472, 4, "101");
    flexSheet.setCellData(472, 6, "Average");
    flexSheet.setCellData(473, 2, "2");
    flexSheet.setCellData(473, 4, "102");
    flexSheet.setCellData(473, 6, "Count");
    flexSheet.setCellData(474, 2, "3");
    flexSheet.setCellData(474, 4, "103");
    flexSheet.setCellData(474, 6, "CountA");
    flexSheet.setCellData(475, 2, "4");
    flexSheet.setCellData(475, 4, "104");
    flexSheet.setCellData(475, 6, "Max");
    flexSheet.setCellData(476, 2, "5");
    flexSheet.setCellData(476, 4, "105");
    flexSheet.setCellData(476, 6, "Min");
    flexSheet.setCellData(477, 2, "6");
    flexSheet.setCellData(477, 4, "106");
    flexSheet.setCellData(477, 6, "Product");
    flexSheet.setCellData(478, 2, "7");
    flexSheet.setCellData(478, 4, "107");
    flexSheet.setCellData(478, 6, "Stdev");
    flexSheet.setCellData(479, 2, "8");
    flexSheet.setCellData(479, 4, "108");
    flexSheet.setCellData(479, 6, "StdevP");
    flexSheet.setCellData(480, 2, "9");
    flexSheet.setCellData(480, 4, "109");
    flexSheet.setCellData(480, 6, "Sum");
    flexSheet.setCellData(481, 2, "10");
    flexSheet.setCellData(481, 4, "110");
    flexSheet.setCellData(481, 6, "Var");
    flexSheet.setCellData(482, 2, "11");
    flexSheet.setCellData(482, 4, "111");
    flexSheet.setCellData(482, 6, "VarP");
    flexSheet.setCellData(483, 1, "Sample:");
    flexSheet.setCellData(483, 2, "subtotal(3, B388:D390, G388:G391)");
    flexSheet.setCellData(484, 1, "Result:");
    flexSheet.setCellData(484, 2, "=subtotal(3, B388:D390, G388:G391)");
    flexSheet.setCellData(485, 1, "Sample:");
    flexSheet.setCellData(485, 2, "subtotal(6, E393:F393)");
    flexSheet.setCellData(486, 1, "Result:");
    flexSheet.setCellData(486, 2, "=subtotal(6, E393:F393)");
    flexSheet.setCellData(488, 0, "3. Lookup & Reference");
    flexSheet.setCellData(489, 1, "3.1. Index");
    flexSheet.setCellData(490, 1, "Uses an index to choose a value from a reference.");
    flexSheet.setCellData(491, 1, "Syntax:");
    flexSheet.setCellData(491, 2, "index(range,row_num,[col_num])");
    flexSheet.setCellData(492, 1, "Remarks:");
    flexSheet.setCellData(492, 2, "If row_num or column_num to 0, inedx returns the array of values for the entire column or row.");
    flexSheet.setCellData(493, 1, "Sample:");
    flexSheet.setCellData(493, 2, "index(B393:F394, 2, 2)");
    flexSheet.setCellData(494, 1, "Result:");
    flexSheet.setCellData(494, 2, "=index(B393:F394, 2, 2)");
    flexSheet.setCellData(495, 1, "Sample:");
    flexSheet.setCellData(495, 2, "sum(index(C393:D402, 0, 1))");
    flexSheet.setCellData(496, 1, "Result:");
    flexSheet.setCellData(496, 2, "=sum(index(C393:D402, 0, 1))");
    flexSheet.setCellData(498, 1, "3.2. HLookup");
    flexSheet.setCellData(499, 1, "Looks in the top row of an array and returns the value of the indicated cell.");
    flexSheet.setCellData(500, 1, "Syntax:");
    flexSheet.setCellData(500, 2, "hlookup(lookup_value, range, row_index_num, [range_lookup])");
    flexSheet.setCellData(501, 1, "Remarks:");
    flexSheet.setCellData(501, 2, "range_lookup is a logical value that specifies whether you want HLOOKUP to find an exact match or an approximate match.");
    flexSheet.setCellData(502, 2, "If TRUE or omitted, an approximate match is returned.  In other words, if an exact match is not found, the next largest value that is less than lookup_value is returned.");
    flexSheet.setCellData(503, 2, "If FALSE, HLOOKUP will find an exact match.");
    flexSheet.setCellData(504, 2, "If range_lookup is FALSE and lookup_value is text, you can use the wildcard characters, question mark (?) and asterisk (*).");
    flexSheet.setCellData(505, 1, "Sample Data:");
    flexSheet.setCellData(506, 1, "4Test");
    flexSheet.setCellData(506, 2, "Test4");
    flexSheet.setCellData(506, 3, "4Test4");
    flexSheet.setCellData(506, 4, "44Test4");
    flexSheet.setCellData(506, 5, "4Test44");
    flexSheet.setCellData(507, 1, "1");
    flexSheet.setCellData(507, 2, "101");
    flexSheet.setCellData(507, 3, "1001");
    flexSheet.setCellData(507, 4, "5001");
    flexSheet.setCellData(507, 5, "10001");
    flexSheet.setCellData(508, 1, "0.1");
    flexSheet.setCellData(508, 2, "0.2");
    flexSheet.setCellData(508, 3, "0.3");
    flexSheet.setCellData(508, 4, "0.5");
    flexSheet.setCellData(508, 5, "0.8");
    flexSheet.setCellData(509, 1, "Sample:");
    flexSheet.setCellData(509, 2, "hlookup(7500, B508:F509, 2)");
    flexSheet.setCellData(510, 1, "Result:");
    flexSheet.setCellData(510, 2, "=hlookup(7500, B508:F509, 2)");
    flexSheet.setCellData(511, 1, "Sample:");
    flexSheet.setCellData(511, 2, "hlookup(\"?test?\", B507:F509, 3, false)");
    flexSheet.setCellData(512, 1, "Result:");
    flexSheet.setCellData(512, 2, "=hlookup(\"?test?\", B507:F509, 3, false)");
    flexSheet.setCellData(514, 0, "4. Financial");
    flexSheet.setCellData(515, 1, "4.1. Rate");
    flexSheet.setCellData(516, 1, "Returns the interest rate per period of an annuity.");
    flexSheet.setCellData(517, 1, "Syntax:");
    flexSheet.setCellData(517, 2, "rate(nper, pmt, pv, [fv], [type], [guess])");
    flexSheet.setCellData(518, 1, "The rate function syntax has the following arguments:");
    flexSheet.setCellData(519, 2, "nper:");
    flexSheet.setCellData(519, 3, "The total number of payment periods in an annuity.");
    flexSheet.setCellData(520, 2, "pmt:");
    flexSheet.setCellData(520, 3, "The payment made each period and cannot change over the life of the annuity.");
    flexSheet.setCellData(521, 2, "pv:");
    flexSheet.setCellData(521, 3, "The total amount that a series of future payments is worth now.");
    flexSheet.setCellData(522, 2, "fv:");
    flexSheet.setCellData(522, 3, "The future value, or a cash balance you want to attain after the last payment is made.");
    flexSheet.setCellData(523, 2, "type:");
    flexSheet.setCellData(523, 3, "The number 0 or 1 and indicates when payments are due.");
    flexSheet.setCellData(524, 3, "0 or omitted means at the end of the period.");
    flexSheet.setCellData(525, 3, "1 means at the beginning of the period.");
    flexSheet.setCellData(526, 2, "guess:");
    flexSheet.setCellData(526, 3, "Your guess for what the rate will be.  If you omit guess, it is assumed to be 10 percent.");
    flexSheet.setCellData(527, 1, "Sample:");
    flexSheet.setCellData(527, 2, "rate(48, -200, 8000)");
    flexSheet.setCellData(528, 1, "Result:");
    flexSheet.setCellData(528, 2, "=rate(48, -200, 8000)");
}

// Apply styles for the formulas sheet.
function applyStyleForFormulasSheet(flexSheet) {
    flexSheet.rows[0].height = 30;
    flexSheet.rows[32].height = 30;
    flexSheet.rows[102].height = 30;
    flexSheet.rows[132].height = 30;
    flexSheet.rows[211].height = 30;
    flexSheet.applyCellsStyle({
        fontSize: '16px',
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(0, 0, 0, 2),
        new wijmo.grid.CellRange(32, 0, 32, 1),
        new wijmo.grid.CellRange(118, 0, 118, 1),
        new wijmo.grid.CellRange(148, 0, 148, 1),
        new wijmo.grid.CellRange(227, 0, 227, 1),
        new wijmo.grid.CellRange(297, 0, 297, 1),
        new wijmo.grid.CellRange(314, 0, 314, 1),
        new wijmo.grid.CellRange(341, 0, 342, 1),
        new wijmo.grid.CellRange(385, 0, 385, 1),
        new wijmo.grid.CellRange(488, 0, 488, 1),
        new wijmo.grid.CellRange(514, 0, 514, 0)]);
    flexSheet.applyCellsStyle({
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(1, 1, 1, 2),
        new wijmo.grid.CellRange(5, 1, 5, 2),
        new wijmo.grid.CellRange(10, 1, 10, 2),
        new wijmo.grid.CellRange(15, 1, 15, 2),
        new wijmo.grid.CellRange(19, 1, 19, 2),
        new wijmo.grid.CellRange(24, 1, 24, 2),
        new wijmo.grid.CellRange(28, 1, 28, 1),
        new wijmo.grid.CellRange(33, 1, 33, 1),
        new wijmo.grid.CellRange(37, 1, 37, 1),
        new wijmo.grid.CellRange(41, 1, 41, 1),
        new wijmo.grid.CellRange(45, 1, 45, 1),
        new wijmo.grid.CellRange(49, 1, 49, 1),
        new wijmo.grid.CellRange(53, 1, 53, 1),
        new wijmo.grid.CellRange(57, 1, 57, 1),
        new wijmo.grid.CellRange(61, 1, 61, 1),
        new wijmo.grid.CellRange(65, 1, 65, 1),
        new wijmo.grid.CellRange(69, 1, 69, 1),
        new wijmo.grid.CellRange(73, 1, 73, 1),
        new wijmo.grid.CellRange(77, 1, 77, 1),
        new wijmo.grid.CellRange(81, 1, 81, 1),
        new wijmo.grid.CellRange(86, 1, 86, 1),
        new wijmo.grid.CellRange(90, 1, 90, 1),
        new wijmo.grid.CellRange(94, 1, 94, 1),
        new wijmo.grid.CellRange(98, 1, 98, 1),
        new wijmo.grid.CellRange(102, 1, 102, 1),
        new wijmo.grid.CellRange(106, 1, 106, 1),
        new wijmo.grid.CellRange(110, 1, 110, 1),
        new wijmo.grid.CellRange(114, 1, 114, 1),
        new wijmo.grid.CellRange(119, 1, 119, 1),
        new wijmo.grid.CellRange(123, 1, 123, 1),
        new wijmo.grid.CellRange(127, 1, 127, 1),
        new wijmo.grid.CellRange(131, 1, 131, 1),
        new wijmo.grid.CellRange(135, 1, 135, 1),
        new wijmo.grid.CellRange(139, 1, 139, 1),
        new wijmo.grid.CellRange(143, 1, 143, 1),
        new wijmo.grid.CellRange(149, 1, 149, 1),
        new wijmo.grid.CellRange(153, 1, 153, 1),
        new wijmo.grid.CellRange(157, 1, 157, 1),
        new wijmo.grid.CellRange(162, 1, 162, 1),
        new wijmo.grid.CellRange(166, 1, 166, 1),
        new wijmo.grid.CellRange(170, 1, 170, 1),
        new wijmo.grid.CellRange(175, 1, 175, 1),
        new wijmo.grid.CellRange(179, 1, 179, 1),
        new wijmo.grid.CellRange(184, 1, 184, 1),
        new wijmo.grid.CellRange(189, 1, 189, 1),
        new wijmo.grid.CellRange(193, 1, 193, 1),
        new wijmo.grid.CellRange(197, 1, 197, 1),
        new wijmo.grid.CellRange(201, 1, 201, 1),
        new wijmo.grid.CellRange(205, 1, 205, 1),
        new wijmo.grid.CellRange(210, 1, 210, 1),
        new wijmo.grid.CellRange(215, 1, 215, 1),
        new wijmo.grid.CellRange(219, 1, 219, 1),
        new wijmo.grid.CellRange(223, 1, 223, 1),
        new wijmo.grid.CellRange(234, 1, 234, 1),
        new wijmo.grid.CellRange(241, 1, 241, 1),
        new wijmo.grid.CellRange(248, 1, 248, 1),
        new wijmo.grid.CellRange(255, 1, 255, 1),
        new wijmo.grid.CellRange(262, 1, 262, 1),
        new wijmo.grid.CellRange(269, 1, 269, 1),
        new wijmo.grid.CellRange(276, 1, 276, 1),
        new wijmo.grid.CellRange(283, 1, 283, 1),
        new wijmo.grid.CellRange(290, 1, 290, 1),
        new wijmo.grid.CellRange(298, 1, 298, 1),
        new wijmo.grid.CellRange(302, 1, 302, 1),
        new wijmo.grid.CellRange(306, 1, 306, 1),
        new wijmo.grid.CellRange(310, 1, 310, 1),
        new wijmo.grid.CellRange(315, 1, 315, 1),
        new wijmo.grid.CellRange(319, 1, 319, 1),
        new wijmo.grid.CellRange(324, 1, 324, 1),
        new wijmo.grid.CellRange(328, 1, 328, 1),
        new wijmo.grid.CellRange(332, 1, 332, 1),
        new wijmo.grid.CellRange(336, 1, 336, 1),
        new wijmo.grid.CellRange(343, 1, 343, 1),
        new wijmo.grid.CellRange(347, 1, 347, 1),
        new wijmo.grid.CellRange(352, 1, 352, 1),
        new wijmo.grid.CellRange(357, 1, 357, 1),
        new wijmo.grid.CellRange(362, 1, 362, 1),
        new wijmo.grid.CellRange(366, 1, 371, 1),
        new wijmo.grid.CellRange(403, 1, 403, 1),
        new wijmo.grid.CellRange(408, 1, 408, 2),
        new wijmo.grid.CellRange(413, 1, 413, 1),
        new wijmo.grid.CellRange(421, 1, 421, 1),
        new wijmo.grid.CellRange(427, 1, 427, 1),
        new wijmo.grid.CellRange(433, 1, 433, 1),
        new wijmo.grid.CellRange(442, 1, 442, 1),
        new wijmo.grid.CellRange(448, 1, 448, 1),
        new wijmo.grid.CellRange(458, 1, 458, 1),
        new wijmo.grid.CellRange(465, 1, 465, 1),
        new wijmo.grid.CellRange(470, 2, 471, 6),
        new wijmo.grid.CellRange(489, 1, 489, 1),
        new wijmo.grid.CellRange(498, 1, 498, 1),
        new wijmo.grid.CellRange(515, 1, 515, 1),
        new wijmo.grid.CellRange(519, 2, 526, 2)]);
    flexSheet.applyCellsStyle({
        textAlign: 'right'
    }, [new wijmo.grid.CellRange(3, 1, 3, 1), new wijmo.grid.CellRange(3, 3, 3, 3),
        new wijmo.grid.CellRange(7, 1, 8, 1), new wijmo.grid.CellRange(7, 3, 8, 3),
        new wijmo.grid.CellRange(12, 1, 13, 1), new wijmo.grid.CellRange(12, 3, 13, 3),
        new wijmo.grid.CellRange(17, 1, 17, 1), new wijmo.grid.CellRange(17, 3, 17, 3),
        new wijmo.grid.CellRange(21, 1, 22, 1),
        new wijmo.grid.CellRange(22, 1, 22, 1), new wijmo.grid.CellRange(22, 3, 22, 3),
        new wijmo.grid.CellRange(26, 1, 26, 1), new wijmo.grid.CellRange(26, 3, 26, 3),
        new wijmo.grid.CellRange(30, 1, 30, 1), new wijmo.grid.CellRange(30, 3, 30, 3),
        new wijmo.grid.CellRange(35, 1, 35, 1), new wijmo.grid.CellRange(35, 3, 35, 3),
        new wijmo.grid.CellRange(39, 1, 39, 1), new wijmo.grid.CellRange(39, 3, 39, 3),
        new wijmo.grid.CellRange(43, 1, 43, 1), new wijmo.grid.CellRange(43, 3, 43, 3),
        new wijmo.grid.CellRange(47, 1, 47, 1), new wijmo.grid.CellRange(47, 3, 47, 3),
        new wijmo.grid.CellRange(51, 1, 51, 1), new wijmo.grid.CellRange(51, 3, 51, 3),
        new wijmo.grid.CellRange(55, 1, 55, 1), new wijmo.grid.CellRange(55, 3, 55, 3),
        new wijmo.grid.CellRange(59, 1, 59, 1), new wijmo.grid.CellRange(59, 3, 59, 3),
        new wijmo.grid.CellRange(63, 1, 63, 1), new wijmo.grid.CellRange(63, 3, 63, 3),
        new wijmo.grid.CellRange(67, 1, 67, 1), new wijmo.grid.CellRange(67, 3, 67, 3),
        new wijmo.grid.CellRange(71, 1, 71, 1), new wijmo.grid.CellRange(71, 3, 71, 3),
        new wijmo.grid.CellRange(75, 1, 75, 1), new wijmo.grid.CellRange(75, 3, 75, 3),
        new wijmo.grid.CellRange(79, 1, 80, 1), new wijmo.grid.CellRange(79, 3, 80, 3),
        new wijmo.grid.CellRange(83, 1, 84, 1), new wijmo.grid.CellRange(83, 3, 84, 3),
        new wijmo.grid.CellRange(88, 1, 88, 1), new wijmo.grid.CellRange(88, 3, 88, 3),
        new wijmo.grid.CellRange(92, 1, 92, 1), new wijmo.grid.CellRange(92, 3, 92, 3),
        new wijmo.grid.CellRange(96, 1, 96, 1), new wijmo.grid.CellRange(96, 3, 96, 3),
        new wijmo.grid.CellRange(100, 1, 100, 1), new wijmo.grid.CellRange(100, 3, 100, 3),
        new wijmo.grid.CellRange(104, 1, 104, 1), new wijmo.grid.CellRange(104, 3, 104, 3),
        new wijmo.grid.CellRange(108, 1, 108, 1), new wijmo.grid.CellRange(108, 4, 108, 4),
        new wijmo.grid.CellRange(112, 1, 112, 1), new wijmo.grid.CellRange(112, 4, 112, 4),
        new wijmo.grid.CellRange(116, 1, 116, 1), new wijmo.grid.CellRange(116, 3, 116, 3),
        new wijmo.grid.CellRange(121, 1, 121, 1), new wijmo.grid.CellRange(121, 3, 121, 3),
        new wijmo.grid.CellRange(125, 1, 125, 1), new wijmo.grid.CellRange(125, 3, 125, 3),
        new wijmo.grid.CellRange(129, 1, 129, 1), new wijmo.grid.CellRange(129, 3, 129, 3),
        new wijmo.grid.CellRange(133, 1, 133, 1), new wijmo.grid.CellRange(133, 3, 133, 3),
        new wijmo.grid.CellRange(137, 1, 137, 1), new wijmo.grid.CellRange(137, 3, 137, 3),
        new wijmo.grid.CellRange(141, 1, 141, 1), new wijmo.grid.CellRange(141, 3, 141, 3),
        new wijmo.grid.CellRange(145, 1, 146, 1),
        new wijmo.grid.CellRange(151, 1, 151, 1), new wijmo.grid.CellRange(151, 3, 151, 3),
        new wijmo.grid.CellRange(155, 1, 155, 1), new wijmo.grid.CellRange(155, 3, 155, 3),
        new wijmo.grid.CellRange(159, 1, 160, 1),
        new wijmo.grid.CellRange(164, 1, 164, 1), new wijmo.grid.CellRange(164, 3, 164, 3),
        new wijmo.grid.CellRange(168, 1, 168, 1), new wijmo.grid.CellRange(168, 3, 168, 3),
        new wijmo.grid.CellRange(172, 1, 173, 1),
        new wijmo.grid.CellRange(177, 1, 177, 1), new wijmo.grid.CellRange(177, 3, 177, 3),
        new wijmo.grid.CellRange(181, 1, 182, 1),
        new wijmo.grid.CellRange(186, 1, 187, 1),
        new wijmo.grid.CellRange(191, 1, 191, 1), new wijmo.grid.CellRange(191, 3, 191, 3),
        new wijmo.grid.CellRange(195, 1, 195, 1), new wijmo.grid.CellRange(195, 3, 195, 3),
        new wijmo.grid.CellRange(199, 1, 199, 1), new wijmo.grid.CellRange(199, 3, 199, 3),
        new wijmo.grid.CellRange(203, 1, 203, 1), new wijmo.grid.CellRange(203, 3, 203, 3),
        new wijmo.grid.CellRange(207, 1, 208, 1),
        new wijmo.grid.CellRange(212, 1, 213, 1),
        new wijmo.grid.CellRange(217, 1, 217, 1), new wijmo.grid.CellRange(217, 3, 217, 3),
        new wijmo.grid.CellRange(221, 1, 221, 1), new wijmo.grid.CellRange(221, 3, 221, 3),
        new wijmo.grid.CellRange(225, 1, 225, 1), new wijmo.grid.CellRange(225, 3, 225, 3),
        new wijmo.grid.CellRange(236, 1, 239, 1),
        new wijmo.grid.CellRange(243, 1, 246, 1),
        new wijmo.grid.CellRange(250, 1, 253, 1),
        new wijmo.grid.CellRange(257, 1, 260, 1),
        new wijmo.grid.CellRange(264, 1, 267, 1),
        new wijmo.grid.CellRange(271, 1, 274, 1),
        new wijmo.grid.CellRange(278, 1, 281, 1),
        new wijmo.grid.CellRange(285, 1, 288, 1),
        new wijmo.grid.CellRange(292, 1, 295, 1),
        new wijmo.grid.CellRange(300, 1, 300, 1), new wijmo.grid.CellRange(300, 3, 300, 3),
        new wijmo.grid.CellRange(304, 1, 304, 1), new wijmo.grid.CellRange(304, 3, 304, 3),
        new wijmo.grid.CellRange(308, 1, 308, 1), new wijmo.grid.CellRange(308, 3, 308, 3),
        new wijmo.grid.CellRange(312, 1, 312, 1), new wijmo.grid.CellRange(312, 3, 312, 3),
        new wijmo.grid.CellRange(317, 1, 317, 1), new wijmo.grid.CellRange(317, 3, 317, 3),
        new wijmo.grid.CellRange(321, 1, 322, 1),
        new wijmo.grid.CellRange(326, 1, 326, 1), new wijmo.grid.CellRange(326, 3, 326, 3),
        new wijmo.grid.CellRange(330, 1, 330, 1), new wijmo.grid.CellRange(330, 3, 330, 3),
        new wijmo.grid.CellRange(334, 1, 334, 1), new wijmo.grid.CellRange(334, 3, 334, 3),
        new wijmo.grid.CellRange(338, 1, 338, 1), new wijmo.grid.CellRange(338, 3, 338, 3),
        new wijmo.grid.CellRange(345, 1, 345, 1), new wijmo.grid.CellRange(345, 3, 345, 3),
        new wijmo.grid.CellRange(349, 1, 350, 1),
        new wijmo.grid.CellRange(354, 1, 355, 1),
        new wijmo.grid.CellRange(359, 1, 360, 1), new wijmo.grid.CellRange(359, 3, 360, 3),
        new wijmo.grid.CellRange(364, 1, 364, 1),
        new wijmo.grid.CellRange(366, 1, 383, 1),
        new wijmo.grid.CellRange(405, 1, 406, 1),
        new wijmo.grid.CellRange(410, 1, 411, 1),
        new wijmo.grid.CellRange(415, 1, 419, 1),
        new wijmo.grid.CellRange(423, 1, 425, 1),
        new wijmo.grid.CellRange(429, 1, 431, 1),
        new wijmo.grid.CellRange(435, 1, 440, 1),
        new wijmo.grid.CellRange(444, 1, 446, 1),
        new wijmo.grid.CellRange(450, 1, 451, 1), new wijmo.grid.CellRange(453, 1, 456, 1),
        new wijmo.grid.CellRange(460, 1, 463, 1),
        new wijmo.grid.CellRange(467, 1, 468, 1), new wijmo.grid.CellRange(483, 1, 486, 1),
        new wijmo.grid.CellRange(491, 1, 496, 1),
        new wijmo.grid.CellRange(500, 1, 504, 1), new wijmo.grid.CellRange(509, 1, 512, 1),
        new wijmo.grid.CellRange(517, 1, 517, 1), new wijmo.grid.CellRange(519, 2, 526, 2), new wijmo.grid.CellRange(527, 1, 528, 1)]);
    flexSheet.applyCellsStyle({
        textAlign: 'center',
        fontWeight: 'bold'
    }, [new wijmo.grid.CellRange(387, 1, 387, 6),
        new wijmo.grid.CellRange(391, 1, 391, 5),
        new wijmo.grid.CellRange(506, 1, 506, 5)]);
    flexSheet.mergeRange(new wijmo.grid.CellRange(0, 0, 0, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(2, 1, 2, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(6, 1, 6, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(10, 1, 10, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(11, 1, 11, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(15, 1, 15, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(16, 1, 16, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(20, 1, 20, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(21, 2, 21, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(24, 1, 24, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(25, 1, 25, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(28, 1, 28, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(29, 1, 29, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(32, 0, 32, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(34, 1, 34, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(38, 1, 38, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(42, 1, 42, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(46, 1, 46, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(50, 1, 50, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(54, 1, 54, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(58, 1, 58, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(62, 1, 62, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(66, 1, 66, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(70, 1, 70, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(74, 1, 74, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(78, 1, 78, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(82, 1, 82, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(87, 1, 87, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(91, 1, 91, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(95, 1, 95, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(99, 1, 99, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(103, 1, 103, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(106, 1, 106, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(107, 1, 107, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(108, 2, 108, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(110, 1, 110, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(111, 1, 111, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(112, 2, 112, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(115, 1, 115, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(118, 0, 118, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(119, 1, 119, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(120, 1, 120, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(124, 1, 124, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(128, 1, 128, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(132, 1, 132, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(136, 1, 136, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(140, 1, 140, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(144, 1, 144, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(145, 2, 145, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(148, 0, 148, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(150, 1, 150, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(154, 1, 154, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(157, 1, 157, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(158, 1, 158, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(159, 2, 159, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(163, 1, 163, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(167, 1, 167, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(171, 1, 171, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(172, 2, 172, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(176, 1, 176, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(180, 1, 180, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(181, 2, 181, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(185, 1, 185, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(186, 2, 186, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(190, 1, 190, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(194, 1, 194, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(198, 1, 198, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(202, 1, 202, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(206, 1, 206, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(207, 2, 207, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(210, 1, 210, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(211, 1, 211, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(212, 2, 212, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(216, 1, 216, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(220, 1, 220, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(224, 1, 224, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(227, 0, 227, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(235, 1, 235, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(236, 2, 236, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(238, 2, 238, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(242, 1, 242, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(243, 2, 243, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(245, 2, 245, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(249, 1, 249, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(250, 2, 250, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(252, 2, 252, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(256, 1, 256, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(257, 2, 257, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(259, 2, 259, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(263, 1, 263, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(266, 2, 266, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(270, 1, 270, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(271, 2, 271, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(273, 2, 273, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(277, 1, 277, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(278, 2, 278, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(280, 2, 280, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(284, 1, 284, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(285, 2, 285, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(287, 2, 287, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(291, 1, 291, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(292, 2, 292, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(294, 2, 294, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(297, 0, 297, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(299, 1, 299, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(300, 4, 300, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(303, 1, 303, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(307, 1, 307, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(311, 1, 311, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(314, 0, 314, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(315, 1, 315, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(316, 1, 316, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(317, 4, 317, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(320, 1, 320, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(321, 2, 321, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(325, 1, 325, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(329, 1, 329, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(333, 1, 333, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(337, 1, 337, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(341, 0, 341, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(342, 0, 342, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(344, 1, 344, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(348, 1, 348, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(349, 2, 349, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(353, 1, 353, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(354, 2, 354, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(358, 1, 358, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(363, 1, 363, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(364, 2, 364, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(365, 1, 365, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(366, 2, 366, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(367, 2, 367, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(368, 2, 368, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(369, 2, 369, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(370, 2, 370, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(371, 2, 371, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(372, 2, 372, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(374, 2, 374, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(376, 2, 376, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(378, 2, 378, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(380, 2, 380, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(382, 2, 382, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(385, 0, 385, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(404, 1, 404, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(405, 2, 405, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(408, 1, 408, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(409, 1, 409, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(410, 2, 410, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(414, 1, 414, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(415, 2, 415, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(416, 2, 416, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(418, 2, 418, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(422, 1, 422, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(423, 2, 423, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(424, 2, 424, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(428, 1, 428, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(429, 2, 429, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(430, 2, 430, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(434, 1, 434, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(435, 2, 435, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(436, 2, 436, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(437, 2, 437, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(439, 2, 439, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(443, 1, 443, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(444, 2, 444, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(445, 2, 445, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(449, 1, 449, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(450, 2, 450, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(451, 2, 451, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(452, 2, 452, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(453, 2, 453, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(455, 2, 455, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(459, 1, 459, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(460, 2, 460, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(462, 2, 462, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(466, 1, 466, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(467, 2, 467, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(468, 2, 468, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(469, 2, 469, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(470, 2, 470, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(470, 4, 470, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(471, 2, 471, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(471, 4, 471, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(483, 2, 483, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(485, 2, 485, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(488, 0, 488, 1));
    flexSheet.mergeRange(new wijmo.grid.CellRange(490, 1, 490, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(491, 2, 491, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(492, 2, 492, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(493, 2, 493, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(495, 2, 495, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(499, 1, 499, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(500, 2, 500, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(501, 2, 501, 9));
    flexSheet.mergeRange(new wijmo.grid.CellRange(502, 2, 502, 11));
    flexSheet.mergeRange(new wijmo.grid.CellRange(503, 2, 503, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(504, 2, 504, 9));
    flexSheet.mergeRange(new wijmo.grid.CellRange(509, 2, 509, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(511, 2, 511, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(516, 1, 516, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(517, 2, 517, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(518, 1, 518, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(519, 3, 519, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(520, 3, 520, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(521, 3, 521, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(522, 3, 522, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(523, 3, 523, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(524, 3, 524, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(525, 3, 525, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(526, 3, 526, 7));
    flexSheet.mergeRange(new wijmo.grid.CellRange(527, 2, 527, 3));
}

// Generate the Table Sheet.
function generateTableSheet(flexSheet) {
    var data = getTableData(10),
        emptyTable,
        tableStyle,
        table;

    flexSheet.setCellData(1, 1, 'To make managing and analyzing a group of related data easier, you can turn a range of cells into a Table.');
    flexSheet.setCellData(2, 1, 'FlexSheet supports both to add empty table and table with data source.');
    flexSheet.setCellData(3, 1, 'If you want to add an empty table, you need create a Table instance then add the Table instance into the tables collection of FlexSheet.');
    flexSheet.setCellData(4, 1, 'The code snippet is as below:');
    flexSheet.setCellData(5, 1, 'let table = new wijmo.grid.sheet.Table(\'Table1\', new wijmo.grid.CellRange(1, 1, 10, 4));');
    flexSheet.setCellData(6, 1, 'flexSheet.selectedSheet.tables.push(table);');
    flexSheet.setCellData(8, 1, 'Following sample shows how add an empty table to FlexSheet');
    flexSheet.setCellData(21, 1, 'FlexSheet also allows to add a table with data source via "addTableFromArray" method.');
    flexSheet.setCellData(22, 1, 'The code snippet is as below:');
    flexSheet.setCellData(23, 1, 'flexSheet.selectedSheet.addTableFromArray(1, 1, dataSource, null, \'Table2\').');
    flexSheet.setCellData(25, 1, 'Following sample shows how add a table with data source:');

    flexSheet.applyCellsStyle({
        fontWeight: 'bold',
        color: 'red'
    }, [new wijmo.grid.CellRange(5, 1, 5, 7),
        new wijmo.grid.CellRange(6, 1, 6, 2),
        new wijmo.grid.CellRange(23, 1, 23, 6)]);

    flexSheet.applyCellsStyle({
        fontStyle: 'italic'
    }, [new wijmo.grid.CellRange(4, 1, 4, 2),
        new wijmo.grid.CellRange(8, 1, 4, 2),
        new wijmo.grid.CellRange(22, 1, 22, 2),
        new wijmo.grid.CellRange(24, 1, 24, 4)]);

    flexSheet.mergeRange(new wijmo.grid.CellRange(1, 1, 1, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(2, 1, 2, 5));
    flexSheet.mergeRange(new wijmo.grid.CellRange(3, 1, 3, 8));
    flexSheet.mergeRange(new wijmo.grid.CellRange(4, 1, 4, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(5, 1, 5, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(6, 1, 6, 3));
    flexSheet.mergeRange(new wijmo.grid.CellRange(8, 1, 8, 4));
    flexSheet.mergeRange(new wijmo.grid.CellRange(21, 1, 21, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(22, 1, 22, 2));
    flexSheet.mergeRange(new wijmo.grid.CellRange(23, 1, 23, 6));
    flexSheet.mergeRange(new wijmo.grid.CellRange(25, 1, 25, 4));

    emptyTable = new wijmo.grid.sheet.Table('Table1', new wijmo.grid.CellRange(9, 1, 18, 5));
    flexSheet.selectedSheet.tables.push(emptyTable);

    tableStyle = flexSheet.getBuiltInTableStyle('TableStyleDark9');
    table = flexSheet.selectedSheet.addTableFromArray(26, 1, data, null, 'Table2', tableStyle, { showTotalRow: true });
}

function getTableData(count){
    var data = new wijmo.collections.ObservableArray(),
        i = 0,
        country,
        product;

    var countries = ['US', 'Germany', 'UK', 'Japan', 'Italy', 'Greece'],
        products = ['Widget', 'Gadget', 'Doohickey'];

    for (var i = 0; i < count; i++) {
        country = countries[Math.floor(Math.random() * countries.length)];
        product = products[Math.floor(Math.random() * products.length)];
        data.push({
            ID: i,
            Country: country,
            Product: product,
            Date: new Date(2014, i % 12, i % 28),
            Amount: Math.random() * 10000
        });
    }
    return data;
}