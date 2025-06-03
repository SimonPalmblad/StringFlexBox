# Flexible textboxes for strings
This library is used to create strings formatted inside of textboxes. Any string input inside a flexible textbox will be automatically word-wrapped to fit the supplied size of the textbox.


# Demo

## 1. Simple word-wrapped textboxes
### 1A.) A textbox 20 characters wide with no padding
```
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

### 1B.) A textbox 20 characters wide with a padding of 3
```
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

### 1C.) A textbox 40 characters wide with a padding of 3
```
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

## 2. Any TextBox can be organized inside of another flexible box

### Either horizontally
```
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
### Or vertically
```
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

### Here's a horizontal box with the two previous boxes stored inside
```
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

## Customize the borders of your boxes using the FlexBoxBorder Class

### Remove borders using the FlexBoxBorder.None
The first textbox and the partent box have no borders, but they still apply padding.
```
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

### Create custom borders using the FlexBoxBorder Class
```
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

## Customizable padding options

### Padding can be uniquely set for each side of the box as well
Using the Padding Class, it's possible to control all aspects of a box's padding. To implement custom padding sizes, you will have to define your sizes in the box's constructor by supplying a Padding.
```
//Padding with uniform horizontal sizes and unifrom vertical sizes.
Padding uniformAxes = new Padding(horizontalPadding: 3, verticalPadding: 10, centerPadding: 0)

// Padding with all sides customized
Padding allSidesCustom = new Padding(left: 1, right: 8, top: 2, bottom: 12, 0)

```
Creating the boxes with custom padding:
```
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