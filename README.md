# Image Editor developed in C#

## JPEC Comopression
Custom JPEG compression using following steps.

### Compression technique.
	1)  Translate RGB to YCrCb
	2)  Reduce colour components CrCb Subsample by taking every other pixel value in the colour component
	3)  Break the pixels into 8x8 blocks (values between -128->127). Perform DCT on each block
	4)  Quantize: Divide each DCT value by it's corresponding table value and round to nearest int
  5)  Entropy Encoding

## Image Morpher

### User guide
  1) Draw lines on source frame.
  2) Click "Edit" mode and change the lines of destination frame.
  3) Click "Run" button.
  
### Morphing Technique.
Warping is performed on the source frame to the destination shape(s) using reverse mappping technique.
Reference: https://www.owlnet.rice.edu/~elec539/Projects97/morphjrks/details.html 

## Tech
C#, wpf
