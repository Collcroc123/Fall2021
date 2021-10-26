import maya.cmds as cmds
#Variables
#-Data Types
    #-float - 1.5, 3.664
    #-int - 3, 6, 26
    #-string - "this is a string", "", '', """ """, 'this string has single quotes'
    #-lists - [2,3,5,7], [3,4,2.7,'string stuff',[3,6,7]] - mutable

    #-dictionary - ['shoulder_jnt':[3,6,7], 'elbow':[3,5,7]]
    #-tuple - immutable, similar to list but values cannot change
    #-booleans - True and False
    #-Null - empty

#IF
age = 25
if age < 30:
    print("You're so young!")
elif age < 40:
    print("You've matured rather well sir/ma'am")
elif age < 50:
    print("...")
else:
    print("Now you really are old!")

#FOR
numbers = [2, 5, 7, 3, 1, 6, 7, 4, 3]
name = 'Clayton'
for char in name:
    print(char)

for num in range(len(numbers)):
    print(num)

