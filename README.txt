------------------------------------------
-              PROJECT                   -
------------------------------------------

ALGS4 in C#.NET

------------------------------------------
-              DESCRIPTION               -
------------------------------------------

This project want to provide a C#.NET version of the algorithms described in the book Algorithms, 4th edition.

The original implementation in Java (written by Robert Sedgewick and Kevin Wayne) can be found at the following address:
http://algs4.cs.princeton.edu/code/

The code is released under the GNU General Public License, version 3 (GPLv3)

THIS MATERIALS IS PROTECTED BY COPYRIGHT!

If you wish to license the code under different terms, please contact the authors to discuss.

------------------------------------------
-              MAIN GOALS                -
------------------------------------------

The following are the main goals of this project:
 > Simplify the process of learning algorithms for people with a background in C#.NET
 > Keep the API as close as possible to the original implementation in Java to simplify the study from the book
 > Keep the code similar to the original implementation in Java to simplify the study from the book
 > Use C#.NET coding standards
 > Use C#.NET idioms and special constructs when appropiate

---------------------------------------------------
-       APPROVAL FROM THE ORIGINAL AUTHORS        -
---------------------------------------------------

From: Kevin Wayne (wayne@cs.princeton.edu)
Date: 16 Apr 2013
  To: Andrea Angella (angella.andrea@gmail.com)

Hi Andrea. 

You have our permission to adapt our slides and figures for this project, provided that you acknowledge the source in appropriate ways 
(e.g., referring to the Algorithms, 4th edition textbook). The algs4.jar code is licensed under the GPL, so you are also welcome to 
adapt the code to C# and license it under the same terms (with appropriate acknowledgements). 

I think it would be terrific to have versions of algs4.jar in various languages as useful resource to programmers.

Best,
Kevin


From: Kevin Wayne (wayne@cs.princeton.edu)
Date: 25 Apr 2013
  To: Andrea Angella (angella.andrea@gmail.com)

Hi Andrea. 

Sorry for the delay. 
I'm not aware of any version of algs4.jar in other languages, though I know some students have written versions of some of our data 
types in other languages.

We don't have any formal method for creating other language versions of algs4.jar. 
The code in algs4.jar is licensed under the GPL, so it is possible for anyway just to start up an open-source project and try to build
it slowly. 

Figuring out the right APIs is always the first challenge. 
Mostly, it should be possible to translate over the Java APIs, but I'm sure there will be some C#-specific issues that arise.

We would certainly be happy to plug any such efforts on coursera and encourage others to contribute.

Best,
Kevin

-------------------------------------------------------
-              HOW TO RUN AN ALGORITHM                -
-------------------------------------------------------

First, build the project.

You can use the following syntax to run a particular algorithm:

   algs4 className [args...] < [input.txt]

className is the name of the class that implement an algorithm.
[args...] is the list of arguments to pass to the Main function of the algorithm
[input.txt] is the path to a file that contains input data from the standard input

You can find example of usage at the top of each algorithm class.
The example of usage assume that all the input files are available alongside the algs4 executable.

You can download all the input files at the following address:
http://algs4.cs.princeton.edu/code/algs4-data.zip

