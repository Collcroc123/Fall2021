import maya.cmds as cmds


def WindowMaker(width, height):
    window = cmds.window(title="RIGGER", iconName="icon", widthHeight=(width, height))
    cmds.rowColumnLayout(parent=window, numberOfColumns=2)
    cmds.button(label="Rig", width=width/2, height=height/10, backgroundColor=(0.7,0,0.6))
    cmds.button(label="Control", width=width/2, height=height/10, backgroundColor=(0.9,0.6,0))
    cmds.button(label="Rig 2", width=width/2, height=height/10, backgroundColor=(0,0.8,0.2))
    cmds.button(label="Control 2", width=width/2, height=height/10, backgroundColor=(0,0.6,0.6))
    cmds.showWindow(window)


WindowMaker(250, 500)
