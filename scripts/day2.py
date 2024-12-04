file = open("input.txt", "r", encoding='utf-8-sig')
sum = 0
counter = 0
safe = True
decrease1 = False
increase = False
compare = ""
verschil = 0
countsafe = 0
for line in file.readlines():
    splitint = [int(x) for x in line.split(" ")]
    getal1 = splitint[0]
    getal2 = splitint[1]
    if getal1 > getal2:
        increase = True
    elif getal1 < getal2:
        decrease1 = True
    lengte = len(splitint)
    while lengte > counter + 1:
        verschil = splitint[counter]-splitint[counter+1]
        if verschil < 3:
            safe = True
        elif verschil < 0:
            safe = False
        if safe == False:
            break
        counter += 1
    if safe == True:
        safe = False
        countsafe += 1
print(countsafe)