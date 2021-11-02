import maya.cmds as cmds

def Rename(find, replace):
    objects = cmds.ls(sl=True)
    isInt = False
    if replace >= 0:
        isInt = True
        replaceInt = replace
    for item in objects:
        if isInt is True:
            cmds.rename(item.replace(find, str(replace).zfill(2)))
            if find in item:
                replaceInt += 1
                replace = replaceInt
        else:
            cmds.rename(item.replace(find, replace))

def Name(name, count):
    objects = cmds.ls(sl=True)
    hashNum = ""
    for char in name:
        if char == "#":
            hashNum += "#"
    for item in objects:
        #print(count, item, name, hashNum)
        cmds.rename(item, name.replace(hashNum, str(count).zfill(len(hashNum))))
        count += 1

#Name(Name, Count) Use "##" for nums, Count = starting num
Name("Leg_####_Jnt", 0)

#Rename(Find, Replace) Replace = starting num if want to count up
#Rename("Jnt", 0)