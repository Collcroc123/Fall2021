import maya.cmds as cmds


def Renamer(name, start_num):
    sels = cmds.ls(sl=True)
    new_objs = []

    num_chars = name.count('#')
    name_parts = name.partition('#' * num_chars)

    # Is argument string correctly formatted?
    if not name_parts[2]:
        cmds.error('Argument string requires at least one "#" character. Additional characters')

    # loop through each selected object
    for index, sel in enumerate(sels, start=start_num):
        new_name = name_parts[0] + str(index).zfill(num_chars) + name_parts[2]
        new_name = cmds.rename(sel, new_name)
        new_objs.append(new_name)

    return new_objs

# Renamer('NAME', STARTING INT)   NAME REQUIRES MINIMUM 1 '#'
Renamer('Arm_#_Jnt_##', 1)