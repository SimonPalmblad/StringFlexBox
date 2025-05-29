// See https://aka.ms/new-console-template for more 

using System.Text;

string text = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
string textShort1 = "This is just a little bit of text to keep in a box. 1";
string textShort2 = "This is just a little bit of text to keep in a box. 2";
string textShort3 = "This is just a little bit of text to keep in a box. 3";

TextBox textbox1 = new(text, 50, [3, 2, 0, 0]);
TextBox textbox2 = new(text, 70, 4);

TextBox textbox3 = new(textShort1, 20, 3);
TextBox textbox4 = new(textShort2, 20, 5);
TextBox textbox5 = new(textShort3, 30, 5);

#region Horizontal stacking boxes - WIP ⚠
//BoxOutlineHorizontal outlineHorizontal1 = new(new List<Textboxable>() { textbox3, textbox5 }, 3);
//BoxOutlineHorizontal outlineHorizontal2 = new(new List<Textboxable>() { textbox4, textbox5 }, 4);
//BoxOutline outlineBox = new(new List<Textboxable>() { outlineHorizontal1, outlineHorizontal2 }, 5);

//Console.WriteLine(outlineHorizontal1.FormattedText);
//Console.WriteLine($"Width of {outlineHorizontal1}: {outlineHorizontal1.Width} ");
//Console.WriteLine($"BoxWidth of {outlineHorizontal1}: {outlineHorizontal1.BoxWidth()} ");
//Console.WriteLine($"Width of {textbox3}: {textbox3.BoxWidth()} ");
//Console.WriteLine($"Width of {textbox5}: {textbox5.BoxWidth()} ");
//Console.WriteLine();
//Console.WriteLine(outlineHorizontal2.FormattedText);
//Console.WriteLine(outlineBox.FormattedText);

#endregion

#region Vertical stacking boxes - works 🆗
//Console.WriteLine(textbox1.FormattedText);
//Console.WriteLine($"Width of {textbox1}: {textbox1.Width} ");
//Console.WriteLine($"Height of {textbox1}: {textbox1.Height} ");

BoxOutline outlinedBox1 = new BoxOutline(new List<Textboxable>() { textbox1, textbox2 }, 2);
BoxOutline outlinedBox2 = new BoxOutline(new List<Textboxable>() { textbox3, textbox4 }, 2);

Console.WriteLine(outlinedBox1.FormattedText);
//Console.WriteLine($"Width of {outlinedBox1}: {outlinedBox1.Width} ");
//Console.WriteLine($"Height of {outlinedBox1}: {outlinedBox1.Height} ");

BoxOutline outlinedBoxBox = new BoxOutline(new List<Textboxable>() { outlinedBox1, outlinedBox2}, 5);

//Console.WriteLine(outlinedBoxBox.FormattedText);
//Console.WriteLine($"Width of {outlinedBoxBox}: {outlinedBoxBox.Width} ");
//Console.WriteLine($"Height of {outlinedBoxBox}: {outlinedBoxBox.Height} ");

BoxOutlineHorizontal outlineHoriBox1 = new BoxOutlineHorizontal(new List<Textboxable>() { outlinedBox1, outlinedBox2 }, 5);
BoxOutlineHorizontal outlineHoriBox2 = new BoxOutlineHorizontal(new List<Textboxable>() { outlinedBox2, outlinedBox1 }, 5);

//Console.WriteLine(outlineHoriBox1.FormattedText);
//Console.WriteLine($"Width of {outlineHoriBox1}: {outlineHoriBox1.Width} ");
//Console.WriteLine($"Height of {outlineHoriBox1}: {outlineHoriBox1.Height} ");

BoxOutline outlinedHoriBoxBox = new BoxOutline(new List<Textboxable>() { outlineHoriBox1, outlineHoriBox2 }, 3);
//Console.WriteLine(outlinedHoriBoxBox.FormattedText);

BoxOutlineHorizontal outlineHoriBoxHori = new BoxOutlineHorizontal(new List<Textboxable>() { outlinedHoriBoxBox, outlinedHoriBoxBox }, 2);
//Console.WriteLine(outlineHoriBoxHori.FormattedText);


//Console.WriteLine(outlinedBox2.FormattedText);

//Console.WriteLine($"Width of {outlinedBox2}: {outlinedBox2.Width} ");
//Console.WriteLine($"BoxWidth of {outlinedBox2}: {outlinedBox2.BoxWidth()} ");


//Console.WriteLine(outlinedBoxBox.FormattedText);

//Console.WriteLine($"Width of {outlinedBoxBox}: {outlinedBoxBox.Width} ");
//Console.WriteLine($"BoxWidth of {outlinedBoxBox}: {outlinedBoxBox.BoxWidth()} ");

#endregion



