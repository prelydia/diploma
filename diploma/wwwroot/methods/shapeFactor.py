from functools import reduce
import operator
import sys
import math
from collections import defaultdict
import numpy as np
import matplotlib.pyplot as plt
import re

wwwroot = sys.argv[1] # путь к сетевой папке
status = sys.argv[2] # False # изначально основными частицами являются маленькие частицы
num_segments = int(sys.argv[3]) # 2 # количество квадратов по одной из осей
filename = sys.argv[4] # название сгенерированного файла
foldername = sys.argv[5] # название папки файла 

particles = [[], [], []] # массив для хранения частиц
output = defaultdict(list) # выходной словарь
S = 0 # площадь многоугольника
P = 0 # периметр многоульника
shapeFactor = 0 # коэффициент формы

path = wwwroot + "\\" + foldername + "\\" + filename 

# функция визуализации частиц
def visualisation(matrix, radius):
    
    fig, ax = plt.subplots()
    ax.plot(0, 0,)
    ax.set_aspect('equal')

    # переменные для хранения координат основной частицы
    mainX = 0
    mainY = 0
    for i in range(len(matrix[0])):
        # cond = math.sqrt((matrix[0][i] - mainX)**2 + (matrix[1][i] - mainY)**2) # √((x — x0)² + (y — y0)²) ≤ r
        if (particles[2][i] == mainParticleRadius): # если частица основная, ее цвет - красный
            mainX = matrix[0][i]
            mainY = matrix[1][i]

            # основная частица
            c = plt.Circle ((mainX, mainY), radius= matrix[2][i], color='r') 
            # круг, в пределах которого ищем соседей
            main = plt.Circle ((mainX, mainY), radius= radius, fill = False, color='grey')
            ax.add_patch(c)
            # ax.add_patch(main)
                        
            # рисуем фиксированную ось отсчета
            # ax.plot([mainX, radius], [mainY, matrix[0][i]], 'black')

            # центр частицы
            plt.scatter(mainX, mainY, color='black', s=5)
        # elif (cond <= radius): # если частица вспомогательная, ее цвет - синий
        else: 
            c = plt.Circle ((matrix[0][i], matrix[1][i]), radius= matrix[2][i])
            ax.add_patch(c)
            plt.scatter(matrix[0][i], matrix[1][i], color='black', s=5)

            # рисуем прямую, соединяющую центры изолированной и изолирующей частиц
            # ax.plot([mainX, matrix[0][i]], [mainY, matrix[1][i]], 'b')
    
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'particles.png')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'particles.svg')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'particles.jpeg')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'particles.pdf')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'particles.eps')

    return "\\" + "SFcoordinates" + "\\" + "particles.svg"

# тепловая карта 
# на вход матрица (nxn), где n - квадратов
def heatmap(matrix):

    fig, ax = plt.subplots()
    ax.plot(0, 0,)
    ax.set_aspect('equal')

    # строки и столбцы
    xlabs = list(range(num_segments))
    ylabs = list(range(num_segments))

    ax.set_xticks(np.arange(len(xlabs)), labels = xlabs)
    ax.set_yticks(np.arange(len(ylabs)), labels = ylabs)

    heatmap = plt.pcolor(matrix, cmap='RdYlBu', linewidths=2, edgecolors='k')

    for y in range(matrix.shape[0]):
        for x in range(matrix.shape[1]):
            plt.text(x + 0.5, y + 0.5, matrix[y, x],
                    horizontalalignment='center',
                    verticalalignment='center')

    plt.colorbar(heatmap)
    plt.xticks(np.arange(min(xlabs), max(ylabs), 1.0))
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'heatmap.png')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'heatmap.svg')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'heatmap.pdf')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'heatmap.jpeg')
    plt.savefig(wwwroot + "\\SFcoordinates\\" + 'heatmap.eps')

    return "\\" + "SFcoordinates" + "\\" + "heatmap.svg"

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
            if (len(temp) >= 4):
                center = tuple(map(operator.truediv, reduce(lambda x, y: map(operator.add, x, y), temp), [len(temp)] * 2))
                sorted_temp = sorted(temp, key=lambda coord: (-135 - math.degrees(math.atan2(*tuple(map(operator.sub, coord, center))[::-1]))) % 360)
                sorted_temp.append(sorted_temp[0]) # замыкаем наш многогольник
                output[(particles[0][i], particles[1][i])].append(sorted_temp)

# рассчет коэффициента формы по частицам
factors = defaultdict(list)
for key in output.keys():
    values = output[key][0]
    a = 0 # первая часть числителя
    b = 0 # вторая часть числителя
    S = 0 # площадь многоугольника
    P = 0 # периметр многоугольника
    for inx in range(len(values) - 1):
        a += values[inx][0] * values[inx + 1][1]
        b += values[inx][1] * values[inx + 1][0]
        distance = math.sqrt((values[inx + 1][0] - values[inx][0])**2 + (values[inx + 1][1] - values[inx][1])**2) # √((x — x0)² + (y — y0)²)
        P += distance

    S = abs(a - b)/2
    shapeFactor = (P**2)/(4*math.pi*S)
    factors[key].append(shapeFactor)

# считаем значения по квадратам
lengthX = max(particles[0]) # до по х
lengthY = max(particles[1]) # до по у

# считаем значения по квадратам
r1X = min(particles[0]) # от по х
wX = (lengthX - r1X) / num_segments # ширина квадрата х

r1Y = min(particles[1]) # от по у
wY = (lengthY - r1Y) / num_segments # ширина квадрата у

coordinatesX = list(range(int(r1X), int(lengthX) + 1, int(wX))) # координаты по х
coordinatesY = list(range(int(r1Y), int(lengthY) + 1, int(wY))) # координаты по у

temp = defaultdict(list) # временный словарь для подсчета средних значений параметра
# распределяем значения по квадратам
for key in factors.keys():
    values = factors[key]
    # счетчик для квадратов
    count = 0
    for j in range(len(coordinatesX) - 1):
        for k in range(len(coordinatesY) - 1):
            if ((coordinatesX[j] <= key[0] < coordinatesX[j+1]) and (coordinatesY[k] <= key[1] < coordinatesY[k+1])):
                temp[count].append(values)
            count += 1

# считаем средние значения по квадратам
arrAver = defaultdict(list)
for i in range(num_segments**2):
    if (len(temp[i]) == 0):
        arrAver[i].append(np.NAN)
    else: arrAver[i].append(np.mean(temp[i]))

# преобразуем в массив для тепловой карты
result = np.array(list(arrAver.values())).ravel().reshape(num_segments, num_segments, order='F')

visualPath = visualisation(particles, circleR)
heatmapPath = heatmap(result)

braces = r'[\[\]]'

# сохранение файла с данными heatmap
with open(wwwroot + "\\" + "SFcoordinates" + "\\" + "heatmapData.txt", 'w') as f:
    for count, value in enumerate(result[::-1]):
        f.write(re.sub(braces, '', str(value)) + "\n")

print(visualPath + "\n" + heatmapPath)