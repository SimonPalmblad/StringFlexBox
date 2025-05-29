using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


public class FlexBoxBorder
{
    private char[] borderChars = new char[4];
    private char[] cornerChars = new char[4];

    public FlexBoxBorder(char[] borderCharacters, char[] cornerCharacters)
    {
        borderChars[0] = borderCharacters[0];
        borderChars[1] = borderCharacters[1];
        borderChars[2] = borderCharacters[2];
        borderChars[3] = borderCharacters[3];

        cornerChars[0] = cornerCharacters[0];
        cornerChars[1] = cornerCharacters[1]; 
        cornerChars[2] = cornerCharacters[2];
        cornerChars[3] = cornerCharacters[3]; 
    }

    #region Border Methods
    public char LeftBorder => borderChars[0];
    public char RightBorder => borderChars[1];
    public char TopBorder => borderChars[2];
    public char BottomBorder => borderChars[3];

    public void SetLeftBorder(char character) => borderChars[0] = character;
    public void SetRightBorder(char character) => borderChars[1] = character;
    public void SetTopBorder(char character) => borderChars[2] = character;
    public void SetBottomBorder(char character) => borderChars[3] = character;
    #endregion


    #region Corner Methods
    public char TopLeftCorner => cornerChars[0];
    public char TopRightCorner => cornerChars[1];
    public char BottomLeftCorner => cornerChars[2];
    public char BottomRightCorner => cornerChars[3];

    public void SetTopLeftCorner(char character) => cornerChars[0] = character;
    public void SetTopRightCorner(char character) => cornerChars[1] = character;
    public void SetBottomLeftCorner(char character) => cornerChars[2] = character;
    public void SetBottomRightCorner(char character) => cornerChars[3] = character; 
    #endregion


    public static FlexBoxBorder Default => new FlexBoxBorder(new char[] { '│', '│', '─', '─' }, new char[] { '┌', '┐', '└', '┘' });

}

