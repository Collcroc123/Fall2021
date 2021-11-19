import maya.cmds as cmds
import colorsys as colorsys


def Control(rad, hue, sect, deg):
    objects = cmds.ls(sl=True)
    if objects:
        for item in objects:
            newName = item.rpartition('_')[0] + "_Ctrl"
            CreateControl(item, rad, hue, sect, deg, newName)
    else:
        newName = "Default_Ctrl"
        CreateControl(0, rad, hue, sect, deg, newName)


def CreateControl(item, rad, hue, sect, deg, newName):
    if deg is 0:
        deg = 360
    if sect is 0:
        sect = 30

    newControl = cmds.circle(center=(0, 0, 0), radius=rad, sections=sect, sweep=deg, name=newName, d=1)
    newControl = cmds.listRelatives(newControl)
    group = cmds.group(newControl, name=newName + "_Grp")

    if item is not 0:
        cmds.matchTransform(group, item)

    cmds.setAttr(newControl[0] + ".overrideEnabled", 1)
    cmds.setAttr(newControl[0] + ".overrideRGBColors", 1)
    color = colorsys.hsv_to_rgb(hue / 360, 1, 0.7)
    cmds.setAttr(newControl[0] + ".overrideColorRGB", color[0], color[1], color[2])


# Control(radius=Size, hue=1<->360 sections=NumOfSides(20+circle), sweep=0<->360)
Control(1.5, 282, 30, 360)
