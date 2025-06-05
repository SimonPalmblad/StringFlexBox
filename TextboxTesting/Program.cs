// See https://aka.ms/new-console-template for more 

//#define printTextboxDemo
//#define printBorderDemo
//#define printBorderlessDemo
//#define printCustomPaddingDemo

#define printAddingContentDemo
//#define printResizingDemo

using TextboxRebuild.HelperClasses;

string text = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
string textShort1 = "This is just a little \n bit of text to keep in a box.\n Nr. 1";
string textShort2 = "This is just a little bit of text to keep in a box. Nr. 2";
string textShort3 = "This is just a little bit of text to keep in a box. Nr. 3";

TextBox textbox1 = new(text, 50, new Padding(2));
TextBox textbox2 = new(text, 70, 4);

TextBox textbox3 = new(textShort1, 20, 3);
TextBox textbox4 = new(textShort2, 20, 5);
TextBox textbox5 = new(textShort3, 30, 5);

#region WIP - Resizing
#if printResizingDemo
    textbox3.Resize(25);
    textbox3.Resize(27);

PrintBoxDebug.PrintWithDimensions(textbox3);
#endif
#endregion

#region WIP - Affix/Suffixing
#if printAddingContentDemo
    PrintBoxDebug.PrintWithDimensions(textbox3);

    textbox2.AddLineSuffix(" ~yo");
    PrintBoxDebug.PrintWithDimensions(textbox2);

    textbox3.AddLinePrefix("1. ");
    PrintBoxDebug.PrintWithDimensions(textbox3);

    // Prefixing each row with an incrementing number
    for (int i = 0; i < textbox5.Texts().Count; i++)
    {
        textbox5.AddPrefixAtLine((i + 1).ToString() + ". ", i);
    }
    
    PrintBoxDebug.PrintWithDimensions(textbox5);

    Console.WriteLine(textbox1.Texts().Count);
    for (int i = 0; i < textbox1.Texts().Count; i++)
    {
        textbox1.AddPrefixAtLine((i + 1).ToString() + ". ", i);
    }

    PrintBoxDebug.PrintWithDimensions(textbox1);

//FlexBoxHorizontal horizontalBoxes = new(new List<StringFlexBox> { textbox2, textbox3 }, new Padding(5));
//PrintBoxDebug.PrintWithDimensions(horizontalBoxes);

//FlexBoxVertical verticalBoxes = new(new List<StringFlexBox> { textbox3, textbox2 }, new Padding(2));
//PrintBoxDebug.PrintWithDimensions(verticalBoxes);


#endif
#endregion

#region GitHub README Demos

#region TextBox DEMO
#if printTextBoxDemo
    string textDemoBasic = "This a string inside of a textbox with automatic word wrap.";
    TextBox textBoxDemoBasic = new(textDemoBasic, 20, 0);

    string textDemoPadding = "The text can also have padding added to the box. Here's with a padding of 3.";
    TextBox textBoxDemoPadding = new(textDemoPadding, 20, 3);

    string textDemoSize = "You can set a specific size for your textbox. This is double the size of the previous ones.";
    TextBox textBoxDemoSize = new(textDemoSize, 40, 3);

    FlexBoxHorizontal horizontalBoxes = new(new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(5));
    FlexBoxVertical verticalBoxes = new(new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(2));

    FlexBoxHorizontal horizontalVerticalBoxes = new(new List<StringFlexBox> { verticalBoxes, horizontalBoxes }, new Padding(2));
    Console.WriteLine(horizontalVerticalBoxes.FormattedText);   
#endif
#endregion

#region Borderless DEMO

#if printBorderlessDemo
    string textBorderless1 = "This is a textbox without any borders.";
    TextBox textBoxBorderless1 = new(textBorderless1, 30, 3, FlexBoxBorder.None);
    string textBorderless2 = "This is bordered textbox.";
    TextBox textBoxBorderless2 = new(textBorderless2, 20, 5);

    FlexBoxHorizontal horizontalBorderless = new(new List<StringFlexBox> { textBoxBorderless1, textBoxBorderless2 }, 5, FlexBoxBorder.None);

    Console.WriteLine(horizontalBorderless.FormattedText); 
    
#endif
#endregion

#region Custom border DEMO

#if printBorderDemo
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
#endif
#endregion

#region Custom Padding DEMO
#if printCustomPaddingDemo
    string customPaddingText1 = "This is a textbox with horizontal padding 3 and vertical padding 10.";
    TextBox customPaddingTextBox1 = new(customPaddingText1, 20, new Padding(horizontalPadding: 3, verticalPadding: 10, centerPadding: 0));

    string customPaddingText2 = "A textbox with all paddings in different sizes. left: 1, right: 8, top: 2, bottom: 12, center: 0.";
    TextBox customPaddingTextBox2 = new(customPaddingText2, 20, new Padding(left: 1, right: 8, top: 2, bottom: 12, 0));

    FlexBoxHorizontal horizontalCustomPadding = new(new List<StringFlexBox> { customPaddingTextBox1, customPaddingTextBox2 }, 0);

    Console.WriteLine(horizontalCustomPadding);

#endif
#endregion

#endregion