# -*- coding: utf-8 -*-
import math
# параметр ориентационного порядка связи (IS)
# параметр ориентационного порядка связи (IL)
# на вход принимаются N и Фjks
def count_IS(N, edges):
    count = 0
    x = edges.split(",")

    if ( N != len(x) ):
        count = "Неправильно введены данные!"
    else:
        for i in range(0, N):

            part1 = round(math.cos(N * math.radians(int(x[i]))), 1)
            part2 = 1j*round(math.sin(N * math.radians(int(x[i]))), 1)

            if (abs(part2.imag) == 0.0):
                val = part1 + part2.real
            else:
                val = part1 + part2

            count += val

        count = count/N
    return str(count)

# оценка радиальной однородности
# используем float, потому что проблема перевода чисел с плавающей запятой из python в c#
# на вход принимаются N и Sj
def count_rad(N, Sj):
    x = Sj.split(",")
    x = [eval(i) for i in x]
    averageS = float(sum(x)/len(x))
    count = 0.0

    for i in range(0, N): # от 0 до N
        j = float(abs(float(x[i]) - averageS)/averageS)
        count += j

    count = count/N

    return count

# длина связи
#
def count_len(N, lj):
    x = lj.split(",")
    x = [eval(i) for i in x]
    averageL = float(sum(x)/len(x))
    count = 0.0

    for i in range(0, N): # от 1 до N
        j = float(abs(float(x[i]) - averageL)/averageL)
        count += j

    count = count/N

    return count

# коэффициент формы
def count_coef(A, P):
    count = (P**2)/(4*math.pi*A)
    return count

#print(count_IS(6, "40,60,160,220,280,340"))