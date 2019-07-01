namespace TCP_Listener.ConstVariables
{
    public class StaticPhrases
    {
        public static string WelcomePhrase { get; } = "= = Welcome! = = \n" +
                                               "There are a few commands, that may help you somehow interact with chat.\n" +
                                               " = = = = = = \n" +
                                               "\\chgName [Name] - Changes your remote end point in word given (Max 10 letters) \n" +
                                               "e.x. \\chgName [Server Bot] - Change your remote end point into 'Server Bot'\n" +
                                               " = = = = = = \n" +
                                               "\\chgNameClr [#RGB Colour] - Changes the colour of your name field \n" +
                                               "e.x. \\chgNameClr [#000000] - Name field color will be changed to black\n" +
                                               " = = = = = = \n" +
                                               "\\chgTextClr [#RGB Colour] - Changes the colour of your text field \n" +
                                               "e.x. \\chgTextClr [#ffee00] - Text field color will be changed to yellow\n" +
                                               " = = = = = = \n" +
                                               "\\chgBackgClr [#RGB Colour] - Changes the background colour of your message \n" +
                                               "e.x. \\chgBackgClr [#ff0095] - Background message color will be changed to pink\n" +
                                               " = = = = = = \n" +
                                               "\\RGB - Gives some basic RGB colours in HEX style \n" +
                                               " = = = = = = \n" +
                                               "\\ClrJoke - Read american humour about colors \n" +
                                               " = = = = = = \n" +
                                               "\\help - call this menu again \n" +
                                               " = = = = = = \n" +
                                               "\\rnd - get the new message random style! \n" +
                                               " = = = = = = \n" +
                                               "\\info - get info if you are alone or not :) \n" +
                                               " = = = = = = \n" +
                                               "Respect all the members in chat :)";
        
        public static string[] Jokes { get; } = //0 - 12 (13 jokes)
        {
            "I went to the doctor and said “I keep dreaming my eyes change colour”. The doctor said, “Don’t worry, it’s just a pigment of your imagination”.",
            "A ship load of red paint crashed into a ship load of blue paint. The crews were marooned.",
            "What’s red and smells like blue paint?\n -Red paint.",
            "A friend of mine swallowed some food colouring. He feels he dyed a little inside.",
            "What’s brown and sticky? \n-A stick.",
            "A friend of mine was describing an exotic bird to me and asked what was orange and sounded like a parrot. I told him, “A carrot”.",
            "What’s red and invisible? \n-No tomatoes.",
            "Whiteboards are remarkable.",
            "A friend of mine cooks my making up a recipe and adding a German white wine. It’s an add hock approach to cooking.",
            "What do you call a chameleon that can’t change colors?\n -A reptile dysfunction",
            "- Is white a color? \n- Yes it is. \n- Is black a color? \n- Yes it is. \n- That means I sold you a colored TV!",
            "Why don’t Americans spell “color” like “colour?\n -It was their way of telling Great Britain that they don’t need u.",
            "what color is the sun?\n-I looked at it for a couple of minutes and I think it is black"
        }; 

        public static string RGBColorPhrase { get; } = "= = Basic RGB Colours = = \n" +
                                                       " * Red Colors * \n" +
                                                       "IndianRed #CD5C5C\n" +
                                                       "LightCoral #F08080\n" +
                                                       "LightSalmon	#FFA07A\n" +
                                                       "Crimson	#DC143C\n" +
                                                       "Red	#FF0000\n" +
                                                       "FireBrick #B22222\n" +
                                                       "DarkRed	#8B0000\n" +
                                                       " = = = = = = \n" +
                                                       " * Pink Colors * \n" +
                                                       "Pink #FFC0CB\n" +
                                                       "LightPink #FFB6C1\n" +
                                                       "HotPink	#FF69B4\n" +
                                                       "DeepPink #FF1493\n" +
                                                       " = = = = = = \n" +
                                                       " * Orange Colors * \n" +
                                                       "Coral #FF7F50\n" +
                                                       "Tomato #FF6347\n" +
                                                       "OrangeRed #FF4500\n" +
                                                       "Orange #FFA500\n" +
                                                       " = = = = = = \n" +
                                                       " * Yellow Colors * \n" +
                                                       "Gold #FFD700\n" +
                                                       "Yellow #FFFF00\n" +
                                                       "PapayaWhip #FFEFD5\n" +
                                                       "Moccasin #FFE4B5\n" +
                                                       "PeachPuff #FFDAB9\n" +
                                                       "PaleGoldenrod #EEE8AA\n" +
                                                       "Khaki #F0E68C\n" +
                                                       "DarkKhaki #BDB76B\n" +
                                                       " = = = = = = \n" +
                                                       " * Purple Colors *\n" +
                                                       "Lavender #E6E6FA\n" +
                                                       "Thistle	#D8BFD8\n" +
                                                       "Plum #DDA0DD\n" +
                                                       "Violet #EE82EE\n" +
                                                       "Orchid #DA70D6\n" +
                                                       "Fuchsia	#FF00FF\n" +
                                                       " = = = = = = \n" +
                                                       " * Green Colors *\n" +
                                                       "GreenYellow	#ADFF2F\n" +
                                                       "Chartreuse #7FFF00\n" +
                                                       "LawnGreen #7CFC00\n" +
                                                       "Lime #00FF00\n" +
                                                       "LimeGreen #32CD32\n" +
                                                       "PaleGreen #98FB98\n" +
                                                       "LightGreen #90EE90\n" +
                                                       "SpringGreen	#00FF7F\n" +
                                                       "MediumSeaGreen #3CB371\n" +
                                                       "LightSeaGreen #20B2AA\n" +
                                                       "DarkCyan #008B8B\n" +
                                                       "Teal #008080\n" +
                                                       " = = = = = = \n" +
                                                       "* Blue Colors * \n" +
                                                       "Aqua #00FFFF\n" +
                                                       "Cyan #00FFFF\n" +
                                                       "LightCyan #E0FFFF\n" +
                                                       "PaleTurquoise #AFEEEE\n" +
                                                       "Aquamarine #7FFFD4\n" +
                                                       "Turquoise #40E0D0\n" +
                                                       "DarkTurquoise #00CED1\n" +
                                                       "CadetBlue #5F9EA0\n" +
                                                       "SteelBlue #4682B4\n" +
                                                       "RoyalBlue #4169E1\n" +
                                                       "Blue #0000FF\n" +
                                                       "MediumBlue #0000CD\n" +
                                                       "DarkBlue #00008B\n" +
                                                       "Navy #000080\n" +
                                                       "MidnightBlue #191970\n" +
                                                       " = = = = = = \n" +
                                                       " * Brown Colors * \n" +
                                                       "Cornsilk #FFF8DC\n" +
                                                       "BlanchedAlmond #FFEBCD\n" +
                                                       "Bisque #FFE4C4\n" +
                                                       "NavajoWhite	#FFDEAD\n" +
                                                       "Wheat #F5DEB3\n" +
                                                       "BurlyWood #DEB887\n" +
                                                       "Tan	#D2B48C\n" +
                                                       "RosyBrown #BC8F8F\n" +
                                                       "SandyBrown #F4A460\n" +
                                                       "Goldenrod #DAA520\n" +
                                                       "DarkGoldenrod #B8860B\n" +
                                                       "Brown #A52A2A\n" +
                                                       "Maroon #800000\n" +
                                                       " = = = = = = \n" +
                                                       " * White Colors * \n" +
                                                       "White #FFFFFF\n" +
                                                       "Snow #FFFAFA\n" +
                                                       "FloralWhite	#FFFAF0\n" +
                                                       "Ivory #FFFFF0\n" +
                                                       "AntiqueWhite #FAEBD7\n" +
                                                       "Linen #FAF0E6\n" +
                                                       "LavenderBlush #FFF0F5\n" +
                                                       "MistyRose #FFE4E1\n" +
                                                       " = = = = = = \n" +
                                                       " * Gray Colors * \n" +
                                                       "Gainsboro #DCDCDC\n" +
                                                       "LightGray #D3D3D3\n" +
                                                       "Silver #C0C0C0\n" +
                                                       "DarkGray #A9A9A9\n" +
                                                       "Gray #808080\n" +
                                                       "DimGray	#696969\n" +
                                                       "LightSlateGray #778899\n";


    } 
}