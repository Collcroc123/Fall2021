import maya.cmds as cmds
import random


def RandomScatter(doTimes, minX, maxX, minY, maxY, minZ, maxZ):
    objects = cmds.ls(sl=True)
    for item in objects:
        for i in range(doTimes):
            dupe = cmds.duplicate(item, rr=True)[0]
            x = random.uniform(minX, maxX)
            y = random.uniform(minY, maxY)
            z = random.uniform(minZ, maxZ)
            cmds.move(x, y, z, dupe)
            # cmds.xform(dupe, worldSpace = True, translation = [x, y, z])
            cmds.rotate(random.uniform(0, 360), random.uniform(0, 360), random.uniform(0, 360), dupe)


RandomScatter(100, -25, 25, -25, 25, -25, 25)
