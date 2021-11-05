import maya.cmds as cmds

sels = cmds.ls(sl=True)

print( enumerate(['Clayton','Annika','Bethany','Kaden']))

for sel, i in enumerate(sels):
    print('index is ' + str(i))
    print('selection is ' + sel)