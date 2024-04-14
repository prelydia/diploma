from functools import reduce
import operator
import sys
import math
from collections import defaultdict
import numpy as np

particles = [[], [], []] # массив для хранения частиц
path = 'C:\\Users\\l.khusainova\\Desktop\\методы\\коэффициентФормы\\test.txt' # путь к файлу с координатами
status = "S"
output = defaultdict(list) # выходной словарь
S = 0 # площадь многоугольника
P = 0 # периметр многоульника
shapeFactor = 0 # коэффициент формы

# считываем текстовый файл
with open(path, 'r') as f:
    for lines in f:
        l1 = lines[:-1]
        l = l1.split(' ')
        
        x, y, r = float(l[0]), float(l[1]), float(l[2])
        particles[0].append(x)
        particles[1].append(y)
        particles[2].append(r)

unique_r = list(set(particles[2])) # находим радиусы 

# если в файле кроме S-частиц и L-частиц есть частицы других радиусов
if (len(unique_r) > 2 or len(unique_r) <= 1):
    print("error")
    sys.exit()
else:
    r = min(unique_r) # радиус изолированной S-частицы
    R = max(unique_r) # радиус изолирующих L-частиц
    circleR = r + 2*R

    mainParticleRadius = r if status == "S" else R # меняем радиус основной частицы в зависимости от флажка
    otherParticleRadius = R if status == "S" else r # меняем радиус второстепенной частицы в зависимости от флажка

    # проходимся по списку частиц
    for i in range(len(particles[0])):
        temp = [] # временный массив для значений phi

        if (particles[2][i] == mainParticleRadius): # если частица основная и находится в начале координат

            # проходим по всем остальным вспомогательным частицам
            for j in range(len(particles[0])):
                cond = math.sqrt((particles[0][j] - particles[0][i])**2 + (particles[1][j] - particles[1][i])**2) # √((x — x0)² + (y — y0)²) ≤ r

                if (particles[2][j] == otherParticleRadius and cond <= circleR): # находим вспомогательную частицу и лежит ли точка (центры частиц) в окружности                                 
                    temp.append((particles[0][j], particles[1][j]))

            # сортируем координаты против часовой стрелки (для формулы площади Гаусса)
            center = tuple(map(operator.truediv, reduce(lambda x, y: map(operator.add, x, y), temp), [len(temp)] * 2))
            sorted_temp = sorted(temp, key=lambda coord: (-135 - math.degrees(math.atan2(*tuple(map(operator.sub, coord, center))[::-1]))) % 360)
            sorted_temp.append(sorted_temp[0]) # замыкаем наш многогольник
            output[i].append(sorted_temp)

# рассчет площади образованного многоугольника
for key in range(len(output)):
    values = output[key][0]
    a = 0
    b = 0
    for inx in range(len(values) - 1):
        a += values[inx][0] * values[inx + 1][1]
        b += values[inx][1] * values[inx + 1][0]
    
    S = abs(a - b)/2


# рассчет периметра образованного многоугольника
for key in range(len(output)):
    values = output[key][0]
    for inx in range(len(values) - 1):
        distance = math.sqrt((values[inx + 1][0] - values[inx][0])**2 + (values[inx + 1][1] - values[inx][1])**2) # √((x — x0)² + (y — y0)²)
        P += distance

shapeFactor = (P**2)/(4*math.pi*S)

print(shapeFactor)