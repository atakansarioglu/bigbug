/**
 * @file      main.cpp
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Pseudo C++ source code that sends BigBug message examples.
 *
 * @copyright Copyright (c) 2018 Atakan SARIOGLU ~ www.atakansarioglu.com
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */
 
#include <stdio.h>
#include <iostream>
using namespace std;

int main(void) {
	float x; int hh, mm, a, day;
	
	// Example 1: Implicit descriptor, no payloads.
	puts("HE"); //@BB Hello World!
	// Sends on serial: "HE\n".
	// BigBug displays: "Hello World!"
	
	// Example 2-A: Implicit descriptor, one payload (value type).
	x = -1.250;
	printf("mX%.3f\n", x); //@BB New measurement x={0}
	// Sends on serial: "mX-1.250\n"
	// BigBug displays: "New measurement x=-1.250"
	
	// Example 2-B: ShortCode has been described recently.
	x = 32.768;
	printf("mX%.3f\n", x);
	// Sends on serial: "mX32.768\n"
	// BigBug displays: "New measurement x=32.768"
	
	// Example 3-A: Explicit descriptor, four payloads (value, value, explicit enum, implicit enum).
	hh = 12; mm = 34; a = 1; day = 5;
	//@BB[TM] Time is {0}:{1} {2:(0),(1)AM ,(2)PM }and today is {3:Sun,Mon,Tue,Wed,Thu,Fri,Sat}
    cout << "TM" << hh << " " << mm << " " << a << " " << day << endl; 
	// Sends on serial: "TM12 34 1 5\n"
	// BigBug displays: "Time is 12:34 AM and today is Fri"
	
	// Example 3-B: ShortCode has been described recently.
	hh = 00; mm = 34; a = 0; day = 5;
    cout << "TM" << hh << " " << mm << " " << a << " " << day << endl;
	// Sends on serial: "TM0 34 1 5\n"
	// BigBug displays: "Time is 0:34 and today is Fri"
	
	// Example 4: Explicit descriptor, one payload (value type).
    cout << "Ds" << "BigBug\\ remote\\ printf" << endl;//@BB[Ds] Description: {0}
	// Sends on serial: "DsBigBug\ remote\ printf\n"
	// BigBug displays: "Description: BigBug remote printf"
	
	return 0;
}
