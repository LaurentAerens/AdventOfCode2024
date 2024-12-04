counter = 1
split = "H e l l o , W o r ld ! ".split(" ")
lengte = len(split)

for word in split:
    combine = word + split[counter]
    counter += 1
    if counter == lengte:
        break