/*
 * This is a demo for the StringFlexBox library.
 * - Here you can see how to use the various features and functions available with examples of how they are rendered.
 * - Choose the demo(s) to display by uncommenting their respective definition below.
 * - WIP and experimental features have NOT been tested fully and may cause errors or crashes.
 */

// ______ DEMO DEFINITIONS ______ //
#define useDemo
// #define printTextboxDemo
#define printBorderlessDemo
// #define printBorderDemo
// #define printCustomPaddingDemo

// ______ WIP & EXPERIMENTAL FEATURES ______ //
// #define printResizingDemo
#if useDemo
using StringFlexBox;
class Program 
{
   static void Main()
{

#if printTextboxDemo
string textDemoBasic = "This a string inside of a textbox with automatic word wrap.";
TextBox textBoxDemoBasic = new(textDemoBasic, 20, 0);

string textDemoPadding = "The text can also have padding added to the box. Here's with a padding of 3.";
TextBox textBoxDemoPadding = new(textDemoPadding, 20, 3);

string textDemoSize = "You can set a specific size for your textbox. This is double the size of the previous ones.";
TextBox textBoxDemoSize = new(textDemoSize, 40, 3);

FlexBoxHorizontal horizontalBoxes = new(new List<FlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(5));
FlexBoxVertical verticalBoxes =
 new((List<FlexBox>)new List<FlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(2));

FlexBoxHorizontal horizontalVerticalBoxes = new(new List<FlexBox> { verticalBoxes, horizontalBoxes }, new Padding(2));
Console.WriteLine(horizontalVerticalBoxes.FormattedText);
#endif

#if printBorderlessDemo
    string textBorderless1 = "This is a textbox without any borders. ";
    string textBorderless2 =
 "This is a textbox WITHOUT any borders and no padding. This has more text to test how well it handles word wrapping.";
    TextBox textBoxBorderless1 = new(textBorderless1, 30, 2, FlexBoxBorder.None);
    TextBox textBoxBorderless2 = new(textBorderless2, 30, 0, FlexBoxBorder.None);
    string textBordered1 =
 "This is a bordered textbox without padding. It's going to have quite a lot of text so I can test out line breaks and see if anything breaks.";
    TextBox textBoxBordered1 = new(textBordered1, 20, 0, FlexBoxBorder.Default);
    
    FlexBoxHorizontal horizontalBorderless =
 new(new List<FlexBox> { textBoxBorderless1, textBoxBordered1, textBoxBorderless2 }, 0, FlexBoxBorder.None);
    FlexBoxVertical vertBorderless1 =
 new(new List<FlexBox> { textBoxBorderless1, textBoxBordered1, textBoxBorderless2 }, 0, FlexBoxBorder.None);
    FlexBoxVertical vertBorderless2 =
 new(new List<FlexBox> { textBoxBordered1/*, textBoxBordered1, textBoxBorderless2 */}, 5, FlexBoxBorder.Default);
    FlexBoxHorizontal combined =
 new([vertBorderless1, horizontalBorderless], horizontalBorderless.Padding, FlexBoxBorder.Default);

    Console.WriteLine(horizontalBorderless);
    Console.WriteLine(textBoxBordered1);
    Console.WriteLine(vertBorderless1);

    void PrintTesting()
    {
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
       Console.WriteLine(combined);
    }
    
    bool running = true;
    while (running)
    {
      var input = Console.ReadLine();
      if (input == "exit") running = false;  
      if(input == "baba") PrintTesting();
    }


#endif

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

    FlexBoxHorizontal horizontalCustomBorder =
 new(new List<FlexBox> { textBoxCustomBorder1, textBoxCustomBorder2 }, 5, customBorder);

    Console.WriteLine(horizontalCustomBorder.FormattedText); 

#endif

#if printCustomPaddingDemo
    string customPaddingText1 = "This is a textbox with horizontal padding 3 and vertical padding 10.";
    TextBox customPaddingTextBox1 =
 new(customPaddingText1, 20, new Padding(horizontalPadding: 3, verticalPadding: 10, centerPadding: 0));

    string customPaddingText2 =
 "A textbox with all paddings in different sizes. left: 1, right: 8, top: 2, bottom: 12, center: 0.";
    TextBox customPaddingTextBox2 = new(customPaddingText2, 20, new Padding(left: 1, right: 8, top: 2, bottom: 12, 0));

    FlexBoxHorizontal horizontalCustomPadding =
 new(new List<FlexBox> { customPaddingTextBox1, customPaddingTextBox2 }, 0);

    Console.WriteLine(horizontalCustomPadding);

#endif

#region WIP & Experimental

#if printResizingDemo
#region Resizing
Console.WriteLine(textbox3);
int resize1 = 25;
textbox3.Resize(resize1);

Console.WriteLine($"Resized to: {resize1}");
Console.WriteLine(textbox3);

int resize2 = 27;
Console.WriteLine($"Resized to: {resize2}");
textbox3.Resize(resize2);

Console.WriteLine(textbox3); 
#endregion

#endif
#endregion
}
}
#endif