import maya.cmds as cmds
import colorsys as colorsys


def ColorChange(hue):
    sels = cmds.ls(sl=True)
    for sel in sels:
        shapes = cmds.listRelatives(sel, children=True, shapes=True)
        for shape in shapes:
            cmds.setAttr("%s.overrideEnabled" % shape, True)
            cmds.setAttr("%s.overrideRGBColors" % shape, True)
            print(str(hue) + ", 1, 0.7")
            color = colorsys.hsv_to_rgb(hue / 360, 1, 0.7)
            print(color)
            cmds.setAttr("%s.overrideColorRGB" % shape, color[0], color[1], color[2])
    return


def ColorEdit():
    cmds.colorEditor()
    if cmds.colorEditor(query=True, result=True):
        values = cmds.colorEditor(query=True, hsv=True)
        print("Selected Hue is " + str(values[0]))
        return values[0]


def Rename(name, start_num):
    objects = cmds.ls(sl=True)
    new_objs = []

    num_chars = name.count('#')
    name_parts = name.partition('#' * num_chars)

    # Is argument string correctly formatted?
    if not name_parts[2]:
        cmds.error('Argument string requires at least one "#" character. Additional characters')

    # loop through each selected object
    for index, sel in enumerate(objects, start=start_num):
        new_name = name_parts[0] + str(index).zfill(num_chars) + name_parts[2]
        new_name = cmds.rename(sel, new_name)
        new_objs.append(new_name)

    return new_objs


def SayHello(name):
    print("Hello %s!" % name)
    return
