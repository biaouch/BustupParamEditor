# BustupParamEditor
![Gif](https://cdn.discordapp.com/attachments/316239186736971776/459125743528706070/bustupparamsedit.gif)

By specifying a path to either bustup_param.dat (found in ps3.cpk\bustup\data) or msgAssistBustupParam.dat (data.cpk\font\assist) 
and entering a bustup ID, you can change the values for X and Y positioning of the mouth, eyes, or the entire bustup itself.


The bustup ID consists of 3 parts: Character ID, Expression ID, and Outfit ID. For instance, b002_001_003 is Ryuji grinning wearing a yellow tanktop, where 002 = Ryuji, 001 = Grinning, and 003 = Yellow Tanktop. For a full list of these values, [see this wiki page](https://amicitia.miraheze.org/wiki/Bustup_(P5)).


This program is meant to be used alongside [DDS2Tool](https://github.com/ShrineFox/DDS2Tool), my editor for PS3 bustups. All coordinates are for the top-left pixel of the element that you want to change the alignment of. As an example: 
![Yusuke Bustup](https://i.imgur.com/AEfHoVH.png)

The positioning values for the eyes in this Yusuke bustup (b005_122_00) are (220, 248) because the first pixel of the element lines up with the base image there. So if you wanted to use an image with different dimensions/positioning for the eyes, you'd also want to update the position of the top left pixel so that it still lines up with the face.
