# Task - Ship Rescue

Oh no, captain! Our engine broke and we're stranded at sea! And with the ongoing solar storm that disabled most of our systems, calling for help won't be easy. However, you have a good old-fashioned radio system on board that should still work and you’re not the only one - most other captains carry one as well!

You quickly realize you can establish a channel between you and the shore if other ships forward your communication. That should be easy enough, right? Right?

As barely anyone uses that ancient system for communication, the radios are in various degrees of disrepair and have quite limited range.
You carefully read through the instructions for the radio system and find it has a self-mapping capabilities and can discover all the similar systems in a vast area. Finally some good luck.
After following the instructions to the letter your screen flutters and some text appears on your monitor:

Example Input

AAA_Y: 0,-5,90
BCA_C: 10,20,95
SAC_F: 5,80,65
ARH_B: 100,45,60
XXX_S: 150,70,180

Heh? Ah, of course. "AAA" is a three-letter unique identifier of your ship so each line must correspond to a single found object. But what about the rest? You remember there was something about position and radio range in the instructions, so 0 must be your X coordinate, -5 the Y coordinate, and 90 is the radio range.
But what does "_Y" stand for? When looking through the instructions again you notice a small print: Made in Norway. Warning: There could be compatibility issues between types of vessels where the system is installed.
There are obviously several type of vessels indicated by the suffix and specific compatibility rules. Below is which object types can be contacted from others as you understand it:

(Y)acht -> Y, B, F
(C)ontainer ship -> B, F
(F)ishing boat -> Y,C
(B)uoy -> C,F,B,S
(S)hore -> B, Y

So, when you put it all together, you understand that your ship (Yacht), can contact any other Yacht, Buoy, or Fishing boat in the range of 90 from your position (0,-5). 

Your ship will always be the first line of the input, but the Id might differ…

Your job now is to calculate the shortest path to the shore (there might be more shore systems - any will do). The radio is one-way so you also need to find a path back to establish a duplex channel.

Example Output

AAA, SAC, BCA, ARH, XXX // path to the shore
XXX, AAA // path back
462.34 // total distance
