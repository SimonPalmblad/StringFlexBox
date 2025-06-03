# Flexible textboxes for strings
This library is used to create strings formatted inside of textboxes. Any string input inside a flexible textbox will be automatically word-wrapped to fit the supplied size of the textbox.


# Demo
Below is a demonstration of use-cases for the String Flexbox library. It's meant both as a show-case and a tutorial for using the various functions available inside.
1. [Creating TextBoxes](#1-creating-textboxes)
2. [TextBoxes inside flexible boxes](#2-a-box-within-a-box)
3. [Custom Borders](3-customize-the-borders)
4. [Custom Padding](4-customizable-padding-options)


</br></br>
## 1. Creating TextBoxes
#### 1.a.) A textbox 20 characters wide with no padding
Using the TextBox class you can create a flexible box with automatic word wrap of a given size. The TextBox's main functionality (as the name suggests) is to display text inside a box of given width.
```cs
string textDemoBasic = "This a string inside of a textbox with automatic word wrap.";
TextBox textBoxDemoBasic = new(textDemoBasic, 20, 0);
```

**Result:**
```

┌────────────────────┐
│This a string       │
│inside of a textbox │
│with automatic word │
│wrap.               │
└────────────────────┘
```

#### 1.b.) A textbox 20 characters wide with a padding of 3
Any flexible box can have padding added to sides, which adds an offset between the content and the borders of a given size.
```cs
string textDemoPadding = "The text can also have padding added to the box. Here's with a padding of 3.";
TextBox textBoxDemoPadding = new(textDemoPadding, 20, 3);
```

**Result:**
```
┌──────────────────────────┐
│                          │
│   The text can also      │
│   have padding added     │
│   to the box. Here's     │
│   with a padding of 3.   │
│                          │
└──────────────────────────┘
```

#### 1.c.) A textbox 40 characters wide with a padding of 3
Here's an example of how the box scales with a higher width!
```cs
string textDemoSize = "You can set a specific size for your textbox. This is double the size of the previous ones.";
TextBox textBoxDemoSize = new(textDemoSize, 40, 3);
```

**Result:**
```

┌──────────────────────────────────────────────┐
│                                              │
│   You can set a specific size for your       │
│   textbox. This is double the size of the    │
│   previous ones.                             │
│                                              │
└──────────────────────────────────────────────┘
```
</br></br>
## 2. A box within a box
This is the core of this library, is to be able to easily create dynamic, flexible boxes to organize and display content inside of them.
There are two variants of the container boxes - vertical and horizontal.
- Horizontal flexboxes organize content in **rows**.
- Vertical flexboxes organize contet in **columns**.

#### 2.a.) Either horizontally
Content supplied to a horizontal box will be organized from leftmost (first) to rightmost (last).
```cs
FlexBoxHorizontal horizontalBoxes = 
new(new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding}, new Padding(5));
```

**Result:**
```

┌─────────────────────────────────────────────────────────────────┐
│                                                                 │
│                                                                 │
│     ┌────────────────────┐     ┌──────────────────────────┐     │
│     │This a string       │     │                          │     │
│     │inside of a textbox │     │   The text can also      │     │
│     │with automatic word │     │   have padding added     │     │
│     │wrap.               │     │   to the box. Here's     │     │
│     └────────────────────┘     │   with a padding of 3.   │     │
│                                │                          │     │
│                                └──────────────────────────┘     │
│                                                                 │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```
#### 2.b.) Or vertically
Content inside a horizontal box will be organized from top (first) to bottom (last).
```cs
FlexBoxVertical verticalBoxes = 
new(new List<StringFlexBox> { textBoxDemoBasic, textBoxDemoPadding }, new Padding(2));
```

**Result:**
```
┌────────────────────────────────┐
│                                │
│  ┌────────────────────┐        │
│  │This a string       │        │
│  │inside of a textbox │        │
│  │with automatic word │        │
│  │wrap.               │        │
│  └────────────────────┘        │
│  ┌──────────────────────────┐  │
│  │                          │  │
│  │   The text can also      │  │
│  │   have padding added     │  │
│  │   to the box. Here's     │  │
│  │   with a padding of 3.   │  │
│  │                          │  │
│  └──────────────────────────┘  │
│                                │
└────────────────────────────────┘

```

### 2.c.) Here's a horizontal box with the two previous boxes stored inside
The fun doesn't end here. Any flexbox can (theoretically) store an infinite number of other flexboxes inside of it. In practice, this is limited by font size and display capabilities as any textwrap by the display will break the formatting of the flexboxes.
```cs
FlexBoxHorizontal horizontalVerticalBoxes = 
new (new List<StringFlexBox> {verticalBoxes, horizontalBoxes}, new Padding(2));
```

**Result:**
```
┌───────────────────────────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                                           │
│  ┌────────────────────────────────┐  ┌─────────────────────────────────────────────────────────────────┐  │
│  │                                │  │                                                                 │  │
│  │  ┌────────────────────┐        │  │                                                                 │  │
│  │  │This a string       │        │  │     ┌────────────────────┐     ┌──────────────────────────┐     │  │
│  │  │inside of a textbox │        │  │     │This a string       │     │                          │     │  │
│  │  │with automatic word │        │  │     │inside of a textbox │     │   The text can also      │     │  │
│  │  │wrap.               │        │  │     │with automatic word │     │   have padding added     │     │  │
│  │  └────────────────────┘        │  │     │wrap.               │     │   to the box. Here's     │     │  │
│  │  ┌──────────────────────────┐  │  │     └────────────────────┘     │   with a padding of 3.   │     │  │
│  │  │                          │  │  │                                │                          │     │  │
│  │  │   The text can also      │  │  │                                └──────────────────────────┘     │  │
│  │  │   have padding added     │  │  │                                                                 │  │
│  │  │   to the box. Here's     │  │  │                                                                 │  │
│  │  │   with a padding of 3.   │  │  └─────────────────────────────────────────────────────────────────┘  │
│  │  │                          │  │                                                                       │
│  │  └──────────────────────────┘  │                                                                       │
│  │                                │                                                                       │
│  └────────────────────────────────┘                                                                       │
│                                                                                                           │
└───────────────────────────────────────────────────────────────────────────────────────────────────────────┘
```
</br></br>
## 3. Customize the borders
All boxes can have the characters used to create the border customized. You can do this by defining a custom FlexBoxBorder class.

### 3.a.) Remove borders using the FlexBoxBorder.None
It's easy to replace all borders with white space. The first textbox and the partent box have no borders, but they still apply padding.
```cs
string textBorderless1 = "This is a textbox without any borders.";
TextBox textBoxBorderless1 = new(textBorderless1, 30, 3, FlexBoxBorder.None);

string textBorderless2 = "This is bordered textbox.";
TextBox textBoxBorderless2 = new(textBorderless2, 20, 5);

FlexBoxHorizontal horizontalBorderless = 
new(new List<StringFlexBox> { textBoxBorderless1, textBoxBorderless2 }, 5, FlexBoxBorder.None);
```

**Result:**
```
                                                 ┌──────────────────────────────┐
                                                 │                              │
          This is a textbox without any          │                              │
          borders.                               │     This is bordered         │
                                                 │     textbox.                 │
                                                 │                              │
                                                 │                              │
                                                 └──────────────────────────────┘
```

### 3.b.) Create custom borders using the FlexBoxBorder Class
If you want to use your own characters as borders it's entirely customizable by creating a custom FlexBoxBorder Class.
```cs
FlexBoxBorder customBorder = new FlexBoxBorder
    (
                                    // left, right, top bottom
        borderCharacters: new char[4] { 'a', 'b', 'c', 'd' }, 
        
                                    // topLeft, topRight, botLeft botRight
        cornerCharacters: new char[4] { '1', '2', '3', '4' }
    );
```

**Result:**
```
1ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc2
a                                                                           b
a                                                                           b
a     1cccccccccccccccccccccccccc2     ┌──────────────────────────────┐     b
a     a                          b     │                              │     b
a     a   This is a textbox      b     │                              │     b
a     a   custom borders         b     │     This is just a           │     b
a     a   applied.               b     │     normal textbox.          │     b
a     a                          b     │                              │     b
a     3dddddddddddddddddddddddddd4     │                              │     b
a                                      └──────────────────────────────┘     b
a                                                                           b
a                                                                           b
3ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd4
```
</br></br>
## 4. Customizable padding options
Padding does not have to be uniform. Any side can have a custom value set by the user.

### 4.a.) Padding can be uniquely set for each side of the box as well
Using the Padding Class, it's possible to control all aspects of a box's padding. To implement custom padding sizes, you will have to define your sizes in the box's constructor by supplying a Padding.
```cs
//Padding with uniform horizontal sizes and unifrom vertical sizes.
Padding uniformAxes = new Padding(horizontalPadding: 3, verticalPadding: 10, centerPadding: 0)

// Padding with all sides customized
Padding allSidesCustom = new Padding(left: 1, right: 8, top: 2, bottom: 12, 0)

```
Creating the boxes with custom padding:
Now all you have to do is supply your custom class to any flexible box when creating it.
```cs
string customPaddingText1 = "This is a textbox with different horizontal and vertical padding.";
TextBox customPaddingTextBox1 = new(customPaddingText1, 20, uniformAxes);


string customPaddingText2 = "A textbox with all paddings in different sizes.";
TextBox customPaddingTextBox2 = new(customPaddingText2, 20, allSidesCustom);

FlexBoxHorizontal horizontalCustomPadding = 
new(new List<StringFlexBox> { customPaddingTextBox1, customPaddingTextBox2 }, 5);
```
**Result:**
```
┌───────────────────────────────────────────────────────────┐
│┌──────────────────────────┐┌─────────────────────────────┐│
││                          ││                             ││
││                          ││ A textbox with all          ││
││                          ││ paddings in                 ││
││                          ││ different sizes.            ││
││                          ││ left: 1, right: 8,          ││
││   This is a textbox      ││ top: 2, bottom: 12,         ││
││   with horizontal        ││ center: 0.                  ││
││   padding 3 and          ││                             ││
││   vertical padding 10.   ││                             ││
││                          ││                             ││
││                          ││                             ││
││                          ││                             ││
││                          ││                             ││
││                          │└─────────────────────────────┘│
│└──────────────────────────┘                               │
└───────────────────────────────────────────────────────────┘
```
