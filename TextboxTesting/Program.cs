// See https://aka.ms/new-console-template for more 


string text = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
string textShort1 = "This is just a little bit of text to keep in a box. 1";
string textShort2 = "This is just a little bit of text to keep in a box. 2";
string textShort3 = "This is just a little bit of text to keep in a box. 3";



TextBox textbox1 = new(text, 50, new Padding(10));
TextBox textbox2 = new(text, 70, 4);

TextBox textbox3 = new(textShort1, 20, 3);
TextBox textbox4 = new(textShort2, 20, 5);
TextBox textbox5 = new(textShort3, 30, 5);


#region Vertical stacking boxes - works 🆗
//Console.WriteLine(textbox1.FormattedText);
//Console.WriteLine($"Width of {textbox1}: {textbox1.Width} ");
//Console.WriteLine($"Height of {textbox1}: {textbox1.Height} ");

//BoxVertical outlinedBox1 = new BoxVertical(new List<StringFlexBox>() { textbox1, textbox2 }, 2);
//BoxVertical outlinedBox2 = new BoxVertical(new List<StringFlexBox>() { textbox3, textbox4 }, 2);

//Console.WriteLine(outlinedBox1.FormattedText);
////Console.WriteLine($"Width of {outlinedBox1}: {outlinedBox1.Width} ");
////Console.WriteLine($"Height of {outlinedBox1}: {outlinedBox1.Height} ");

//BoxVertical outlinedBoxBox = new BoxVertical(new List<StringFlexBox>() { outlinedBox1, outlinedBox2}, 5);

////Console.WriteLine(outlinedBoxBox.FormattedText);
////Console.WriteLine($"Width of {outlinedBoxBox}: {outlinedBoxBox.Width} ");
////Console.WriteLine($"Height of {outlinedBoxBox}: {outlinedBoxBox.Height} ");

//BoxHorizontal outlineHoriBox1 = new BoxHorizontal(new List<StringFlexBox>() { outlinedBox1, outlinedBox2 }, 2);
//PrintBoxDebug.PrintWithDimensions(outlineHoriBox1);

//BoxHorizontal outlineHoriBox2 = new BoxHorizontal(new List<StringFlexBox>() { outlinedBox2, outlinedBox1 }, 5);

////Console.WriteLine(outlineHoriBox1.FormattedText);
////Console.WriteLine($"Width of {outlineHoriBox1}: {outlineHoriBox1.Width} ");
////Console.WriteLine($"Height of {outlineHoriBox1}: {outlineHoriBox1.Height} ");

//BoxVertical outlinedHoriBoxBox = new BoxVertical(new List<StringFlexBox>() { outlineHoriBox1, outlineHoriBox2 }, 10);
//PrintBoxDebug.PrintWithDimensions(outlinedHoriBoxBox);

#endregion



#region GitHub README Demos
string textDemoBasic = "This a string inside of a textbox with automatic word wrap.";
TextBox textBoxDemoBasic = new(textDemoBasic, 20, 0);

string textDemoPadding = "The text can also have padding added to the box. Here's with a padding of 3.";
TextBox textBoxDemoPadding = new(textDemoPadding, 20, 3);

string textDemoSize = "You can set a specific size for your textbox. This is double the size of the previous ones.";
TextBox textBoxDemoSize = new(textDemoSize, 40, 3);

FlexBoxHorizontal horizontalBoxes = new (new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(5));
FlexBoxVertical verticalBoxes = new (new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(2));

FlexBoxHorizontal horizontalVerticalBoxes = new (new List<StringFlexBox> {verticalBoxes, horizontalBoxes}, new Padding(2));
Console.WriteLine(horizontalVerticalBoxes.FormattedText);


#region Borderless DEMO
string textBorderless1 = "This is a textbox without any borders.";
TextBox textBoxBorderless1 = new(textBorderless1, 30, 3, FlexBoxBorder.None);
string textBorderless2 = "This is bordered textbox.";
TextBox textBoxBorderless2 = new(textBorderless2, 20, 5);

FlexBoxHorizontal horizontalBorderless = new(new List<StringFlexBox> { textBoxBorderless1, textBoxBorderless2 }, 5, FlexBoxBorder.None);

Console.WriteLine(horizontalBorderless.FormattedText); 
#endregion

#region Custom border DEMO
FlexBoxBorder customBorder = new FlexBoxBorder
(
borderCharacters: new char[4] { 'a', 'b', 'c', 'd' },
cornerCharacters: new char[4] { '1', '2', '3', '4' }
);

string textCustomBorder1 = "This is a textbox custom borders applied.";
TextBox textBoxCustomBorder1 = new(textCustomBorder1, 20, 3, customBorder);

string textCustomBorder2 = "This is just a normal textbox.";
TextBox textBoxCustomBorder2 = new(textCustomBorder2, 20, 5);

FlexBoxHorizontal horizontalCustomBorder = new(new List<StringFlexBox> { textBoxCustomBorder1, textBoxCustomBorder2 }, 5, customBorder);

Console.WriteLine(horizontalCustomBorder.FormattedText);
#endregion

#region Custom Padding DEMO
string customPaddingText1 = "This is a textbox with horizontal padding 3 and vertical padding 10.";
TextBox customPaddingTextBox1 = new(customPaddingText1, 20, new Padding(horizontalPadding: 3, verticalPadding: 10, centerPadding: 0));

string customPaddingText2 = "A textbox with all paddings in different sizes. left: 1, right: 8, top: 2, bottom: 12, center: 0.";
TextBox customPaddingTextBox2 = new(customPaddingText2, 20, new Padding(left: 1, right: 8, top: 2, bottom: 12, 0));

FlexBoxHorizontal horizontalCustomPadding = new(new List<StringFlexBox> { customPaddingTextBox1, customPaddingTextBox2 }, 0);

Console.WriteLine(horizontalCustomPadding);

#endregion

#endregion