// ReSharper disable InconsistentNaming
namespace Rokuro.Input;

public enum Keycode
{
	SDLK_UNKNOWN = 0,
	SDLK_BACKSPACE = 8,
	SDLK_TAB = 9,
	SDLK_RETURN = 13, // 0x0000000D
	SDLK_ESCAPE = 27, // 0x0000001B
	SDLK_SPACE = 32, // 0x00000020
	SDLK_EXCLAIM = 33, // 0x00000021
	SDLK_QUOTEDBL = 34, // 0x00000022
	SDLK_HASH = 35, // 0x00000023
	SDLK_DOLLAR = 36, // 0x00000024
	SDLK_PERCENT = 37, // 0x00000025
	SDLK_AMPERSAND = 38, // 0x00000026
	SDLK_QUOTE = 39, // 0x00000027
	SDLK_LEFTPAREN = 40, // 0x00000028
	SDLK_RIGHTPAREN = 41, // 0x00000029
	SDLK_ASTERISK = 42, // 0x0000002A
	SDLK_PLUS = 43, // 0x0000002B
	SDLK_COMMA = 44, // 0x0000002C
	SDLK_MINUS = 45, // 0x0000002D
	SDLK_PERIOD = 46, // 0x0000002E
	SDLK_SLASH = 47, // 0x0000002F
	SDLK_0 = 48, // 0x00000030
	SDLK_1 = 49, // 0x00000031
	SDLK_2 = 50, // 0x00000032
	SDLK_3 = 51, // 0x00000033
	SDLK_4 = 52, // 0x00000034
	SDLK_5 = 53, // 0x00000035
	SDLK_6 = 54, // 0x00000036
	SDLK_7 = 55, // 0x00000037
	SDLK_8 = 56, // 0x00000038
	SDLK_9 = 57, // 0x00000039
	SDLK_COLON = 58, // 0x0000003A
	SDLK_SEMICOLON = 59, // 0x0000003B
	SDLK_LESS = 60, // 0x0000003C
	SDLK_EQUALS = 61, // 0x0000003D
	SDLK_GREATER = 62, // 0x0000003E
	SDLK_QUESTION = 63, // 0x0000003F
	SDLK_AT = 64, // 0x00000040
	SDLK_LEFTBRACKET = 91, // 0x0000005B
	SDLK_BACKSLASH = 92, // 0x0000005C
	SDLK_RIGHTBRACKET = 93, // 0x0000005D
	SDLK_CARET = 94, // 0x0000005E
	SDLK_UNDERSCORE = 95, // 0x0000005F
	SDLK_BACKQUOTE = 96, // 0x00000060
	SDLK_a = 97, // 0x00000061
	SDLK_b = 98, // 0x00000062
	SDLK_c = 99, // 0x00000063
	SDLK_d = 100, // 0x00000064
	SDLK_e = 101, // 0x00000065
	SDLK_f = 102, // 0x00000066
	SDLK_g = 103, // 0x00000067
	SDLK_h = 104, // 0x00000068
	SDLK_i = 105, // 0x00000069
	SDLK_j = 106, // 0x0000006A
	SDLK_k = 107, // 0x0000006B
	SDLK_l = 108, // 0x0000006C
	SDLK_m = 109, // 0x0000006D
	SDLK_n = 110, // 0x0000006E
	SDLK_o = 111, // 0x0000006F
	SDLK_p = 112, // 0x00000070
	SDLK_q = 113, // 0x00000071
	SDLK_r = 114, // 0x00000072
	SDLK_s = 115, // 0x00000073
	SDLK_t = 116, // 0x00000074
	SDLK_u = 117, // 0x00000075
	SDLK_v = 118, // 0x00000076
	SDLK_w = 119, // 0x00000077
	SDLK_x = 120, // 0x00000078
	SDLK_y = 121, // 0x00000079
	SDLK_z = 122, // 0x0000007A
	SDLK_DELETE = 127, // 0x0000007F
	SDLK_CAPSLOCK = 1073741881, // 0x40000039
	SDLK_F1 = 1073741882, // 0x4000003A
	SDLK_F2 = 1073741883, // 0x4000003B
	SDLK_F3 = 1073741884, // 0x4000003C
	SDLK_F4 = 1073741885, // 0x4000003D
	SDLK_F5 = 1073741886, // 0x4000003E
	SDLK_F6 = 1073741887, // 0x4000003F
	SDLK_F7 = 1073741888, // 0x40000040
	SDLK_F8 = 1073741889, // 0x40000041
	SDLK_F9 = 1073741890, // 0x40000042
	SDLK_F10 = 1073741891, // 0x40000043
	SDLK_F11 = 1073741892, // 0x40000044
	SDLK_F12 = 1073741893, // 0x40000045
	SDLK_PRINTSCREEN = 1073741894, // 0x40000046
	SDLK_SCROLLLOCK = 1073741895, // 0x40000047
	SDLK_PAUSE = 1073741896, // 0x40000048
	SDLK_INSERT = 1073741897, // 0x40000049
	SDLK_HOME = 1073741898, // 0x4000004A
	SDLK_PAGEUP = 1073741899, // 0x4000004B
	SDLK_END = 1073741901, // 0x4000004D
	SDLK_PAGEDOWN = 1073741902, // 0x4000004E
	SDLK_RIGHT = 1073741903, // 0x4000004F
	SDLK_LEFT = 1073741904, // 0x40000050
	SDLK_DOWN = 1073741905, // 0x40000051
	SDLK_UP = 1073741906, // 0x40000052
	SDLK_NUMLOCKCLEAR = 1073741907, // 0x40000053
	SDLK_KP_DIVIDE = 1073741908, // 0x40000054
	SDLK_KP_MULTIPLY = 1073741909, // 0x40000055
	SDLK_KP_MINUS = 1073741910, // 0x40000056
	SDLK_KP_PLUS = 1073741911, // 0x40000057
	SDLK_KP_ENTER = 1073741912, // 0x40000058
	SDLK_KP_1 = 1073741913, // 0x40000059
	SDLK_KP_2 = 1073741914, // 0x4000005A
	SDLK_KP_3 = 1073741915, // 0x4000005B
	SDLK_KP_4 = 1073741916, // 0x4000005C
	SDLK_KP_5 = 1073741917, // 0x4000005D
	SDLK_KP_6 = 1073741918, // 0x4000005E
	SDLK_KP_7 = 1073741919, // 0x4000005F
	SDLK_KP_8 = 1073741920, // 0x40000060
	SDLK_KP_9 = 1073741921, // 0x40000061
	SDLK_KP_0 = 1073741922, // 0x40000062
	SDLK_KP_PERIOD = 1073741923, // 0x40000063
	SDLK_APPLICATION = 1073741925, // 0x40000065
	SDLK_POWER = 1073741926, // 0x40000066
	SDLK_KP_EQUALS = 1073741927, // 0x40000067
	SDLK_F13 = 1073741928, // 0x40000068
	SDLK_F14 = 1073741929, // 0x40000069
	SDLK_F15 = 1073741930, // 0x4000006A
	SDLK_F16 = 1073741931, // 0x4000006B
	SDLK_F17 = 1073741932, // 0x4000006C
	SDLK_F18 = 1073741933, // 0x4000006D
	SDLK_F19 = 1073741934, // 0x4000006E
	SDLK_F20 = 1073741935, // 0x4000006F
	SDLK_F21 = 1073741936, // 0x40000070
	SDLK_F22 = 1073741937, // 0x40000071
	SDLK_F23 = 1073741938, // 0x40000072
	SDLK_F24 = 1073741939, // 0x40000073
	SDLK_EXECUTE = 1073741940, // 0x40000074
	SDLK_HELP = 1073741941, // 0x40000075
	SDLK_MENU = 1073741942, // 0x40000076
	SDLK_SELECT = 1073741943, // 0x40000077
	SDLK_STOP = 1073741944, // 0x40000078
	SDLK_AGAIN = 1073741945, // 0x40000079
	SDLK_UNDO = 1073741946, // 0x4000007A
	SDLK_CUT = 1073741947, // 0x4000007B
	SDLK_COPY = 1073741948, // 0x4000007C
	SDLK_PASTE = 1073741949, // 0x4000007D
	SDLK_FIND = 1073741950, // 0x4000007E
	SDLK_MUTE = 1073741951, // 0x4000007F
	SDLK_VOLUMEUP = 1073741952, // 0x40000080
	SDLK_VOLUMEDOWN = 1073741953, // 0x40000081
	SDLK_KP_COMMA = 1073741957, // 0x40000085
	SDLK_KP_EQUALSAS400 = 1073741958, // 0x40000086
	SDLK_ALTERASE = 1073741977, // 0x40000099
	SDLK_SYSREQ = 1073741978, // 0x4000009A
	SDLK_CANCEL = 1073741979, // 0x4000009B
	SDLK_CLEAR = 1073741980, // 0x4000009C
	SDLK_PRIOR = 1073741981, // 0x4000009D
	SDLK_RETURN2 = 1073741982, // 0x4000009E
	SDLK_SEPARATOR = 1073741983, // 0x4000009F
	SDLK_OUT = 1073741984, // 0x400000A0
	SDLK_OPER = 1073741985, // 0x400000A1
	SDLK_CLEARAGAIN = 1073741986, // 0x400000A2
	SDLK_CRSEL = 1073741987, // 0x400000A3
	SDLK_EXSEL = 1073741988, // 0x400000A4
	SDLK_KP_00 = 1073742000, // 0x400000B0
	SDLK_KP_000 = 1073742001, // 0x400000B1
	SDLK_THOUSANDSSEPARATOR = 1073742002, // 0x400000B2
	SDLK_DECIMALSEPARATOR = 1073742003, // 0x400000B3
	SDLK_CURRENCYUNIT = 1073742004, // 0x400000B4
	SDLK_CURRENCYSUBUNIT = 1073742005, // 0x400000B5
	SDLK_KP_LEFTPAREN = 1073742006, // 0x400000B6
	SDLK_KP_RIGHTPAREN = 1073742007, // 0x400000B7
	SDLK_KP_LEFTBRACE = 1073742008, // 0x400000B8
	SDLK_KP_RIGHTBRACE = 1073742009, // 0x400000B9
	SDLK_KP_TAB = 1073742010, // 0x400000BA
	SDLK_KP_BACKSPACE = 1073742011, // 0x400000BB
	SDLK_KP_A = 1073742012, // 0x400000BC
	SDLK_KP_B = 1073742013, // 0x400000BD
	SDLK_KP_C = 1073742014, // 0x400000BE
	SDLK_KP_D = 1073742015, // 0x400000BF
	SDLK_KP_E = 1073742016, // 0x400000C0
	SDLK_KP_F = 1073742017, // 0x400000C1
	SDLK_KP_XOR = 1073742018, // 0x400000C2
	SDLK_KP_POWER = 1073742019, // 0x400000C3
	SDLK_KP_PERCENT = 1073742020, // 0x400000C4
	SDLK_KP_LESS = 1073742021, // 0x400000C5
	SDLK_KP_GREATER = 1073742022, // 0x400000C6
	SDLK_KP_AMPERSAND = 1073742023, // 0x400000C7
	SDLK_KP_DBLAMPERSAND = 1073742024, // 0x400000C8
	SDLK_KP_VERTICALBAR = 1073742025, // 0x400000C9
	SDLK_KP_DBLVERTICALBAR = 1073742026, // 0x400000CA
	SDLK_KP_COLON = 1073742027, // 0x400000CB
	SDLK_KP_HASH = 1073742028, // 0x400000CC
	SDLK_KP_SPACE = 1073742029, // 0x400000CD
	SDLK_KP_AT = 1073742030, // 0x400000CE
	SDLK_KP_EXCLAM = 1073742031, // 0x400000CF
	SDLK_KP_MEMSTORE = 1073742032, // 0x400000D0
	SDLK_KP_MEMRECALL = 1073742033, // 0x400000D1
	SDLK_KP_MEMCLEAR = 1073742034, // 0x400000D2
	SDLK_KP_MEMADD = 1073742035, // 0x400000D3
	SDLK_KP_MEMSUBTRACT = 1073742036, // 0x400000D4
	SDLK_KP_MEMMULTIPLY = 1073742037, // 0x400000D5
	SDLK_KP_MEMDIVIDE = 1073742038, // 0x400000D6
	SDLK_KP_PLUSMINUS = 1073742039, // 0x400000D7
	SDLK_KP_CLEAR = 1073742040, // 0x400000D8
	SDLK_KP_CLEARENTRY = 1073742041, // 0x400000D9
	SDLK_KP_BINARY = 1073742042, // 0x400000DA
	SDLK_KP_OCTAL = 1073742043, // 0x400000DB
	SDLK_KP_DECIMAL = 1073742044, // 0x400000DC
	SDLK_KP_HEXADECIMAL = 1073742045, // 0x400000DD
	SDLK_LCTRL = 1073742048, // 0x400000E0
	SDLK_LSHIFT = 1073742049, // 0x400000E1
	SDLK_LALT = 1073742050, // 0x400000E2
	SDLK_LGUI = 1073742051, // 0x400000E3
	SDLK_RCTRL = 1073742052, // 0x400000E4
	SDLK_RSHIFT = 1073742053, // 0x400000E5
	SDLK_RALT = 1073742054, // 0x400000E6
	SDLK_RGUI = 1073742055, // 0x400000E7
	SDLK_MODE = 1073742081, // 0x40000101
	SDLK_AUDIONEXT = 1073742082, // 0x40000102
	SDLK_AUDIOPREV = 1073742083, // 0x40000103
	SDLK_AUDIOSTOP = 1073742084, // 0x40000104
	SDLK_AUDIOPLAY = 1073742085, // 0x40000105
	SDLK_AUDIOMUTE = 1073742086, // 0x40000106
	SDLK_MEDIASELECT = 1073742087, // 0x40000107
	SDLK_WWW = 1073742088, // 0x40000108
	SDLK_MAIL = 1073742089, // 0x40000109
	SDLK_CALCULATOR = 1073742090, // 0x4000010A
	SDLK_COMPUTER = 1073742091, // 0x4000010B
	SDLK_AC_SEARCH = 1073742092, // 0x4000010C
	SDLK_AC_HOME = 1073742093, // 0x4000010D
	SDLK_AC_BACK = 1073742094, // 0x4000010E
	SDLK_AC_FORWARD = 1073742095, // 0x4000010F
	SDLK_AC_STOP = 1073742096, // 0x40000110
	SDLK_AC_REFRESH = 1073742097, // 0x40000111
	SDLK_AC_BOOKMARKS = 1073742098, // 0x40000112
	SDLK_BRIGHTNESSDOWN = 1073742099, // 0x40000113
	SDLK_BRIGHTNESSUP = 1073742100, // 0x40000114
	SDLK_DISPLAYSWITCH = 1073742101, // 0x40000115
	SDLK_KBDILLUMTOGGLE = 1073742102, // 0x40000116
	SDLK_KBDILLUMDOWN = 1073742103, // 0x40000117
	SDLK_KBDILLUMUP = 1073742104, // 0x40000118
	SDLK_EJECT = 1073742105, // 0x40000119
	SDLK_SLEEP = 1073742106, // 0x4000011A
	SDLK_APP1 = 1073742107, // 0x4000011B
	SDLK_APP2 = 1073742108, // 0x4000011C
	SDLK_AUDIOREWIND = 1073742109, // 0x4000011D
	SDLK_AUDIOFASTFORWARD = 1073742110 // 0x4000011E
}
