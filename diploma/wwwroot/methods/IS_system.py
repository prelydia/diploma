import random
from collections import defaultdict
import seaborn as sns
from cmath import pi, sqrt
import math
from multiprocessing import Condition
from platform import java_ver
import sys
from turtle import color
import matplotlib.pyplot as plt
import math
import numpy as np
import re
import os

random.seed(0)
particles = [[], [], []] # храним частицы
output = [] # выходной массив с рассчитанными значениями
r = 0 # радиус основной частицы
R = 0 # радиус вспомогательной частицы
circleR = 0 # радиус окружности, куда входят соседи первого порядка
num_segments = int(sys.argv[3]) # 2 # количество квадратов по одной из осей
result = defaultdict(list) # словарь для хранения результатов
status = sys.argv[2] # False # изначально основными частицами являются маленькие частицы
mainParticleRadius = r if status == "S" else R # меняем радиус основной частицы в зависимости от флажка
otherParticleRadius = R if status == "S" else r # меняем радиус второстепенной частицы в зависимости от флажка
wwwroot = sys.argv[1] # путь к сетевой папке
filename = sys.argv[4] # название сгенерированного файла
foldername = sys.argv[5] # название папки файла 
# path = wwwroot + "\\BOPcoordinates\\" # путь к папке с координатами частиц
# path = "C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\BOPcoordinates\\"
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
    
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'particles.png')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'particles.svg')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'particles.jpeg')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'particles.pdf')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'particles.eps')

    return "\\" + "BOPcoordinates" + "\\" + "particles.svg"

# функция для подсчета параметра
def bondOrientationalParameter(edges):
    count = 0 # счетчик
    N = len(edges) # количество соседей

    for i in edges:

        phi = i * N

        cosValue = round(math.cos(math.radians(phi)), 1)
        sinValue = round(math.sin(math.radians(phi)), 1)

        value = complex(cosValue, sinValue)
        count += value

    count = count/N
    return abs(count)

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
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'heatmap.png')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'heatmap.svg')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'heatmap.pdf')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'heatmap.jpeg')
    plt.savefig(wwwroot + "\\BOPcoordinates\\" + 'heatmap.eps')

    return "\\" + "BOPcoordinates" + "\\" + "heatmap.svg"

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

        if (particles[2][i] == mainParticleRadius and (particles[0][i], particles[1][i]) == (0,0)): # если частица основная и находится в начале координат
            # проходим по всем остальным вспомогательным частицам
            for j in range(len(particles[0])):
                cond = math.sqrt((particles[0][j] - particles[0][i])**2 + (particles[1][j] - particles[1][i])**2) # √((x — x0)² + (y — y0)²) ≤ r

                if (particles[2][j] == otherParticleRadius and cond <= circleR): # находим вспомогательную частицу и лежит ли точка (центры частиц) в окружности
                    
                    # using atan2 
                    radians = math.atan2(particles[1][j], particles[0][j]) if math.atan2(particles[1][j], particles[0][j]) >= 0 else math.atan2(particles[1][j], particles[0][j]) + 2*math.pi
                    phi = math.degrees(radians)
                    
                    temp.append(phi)

            output.append((particles[0][i], particles[1][i], bondOrientationalParameter(temp))) if len(temp) != 0 else output.append((particles[0][i], particles[1][i], 0.0))

        elif (particles[2][i] == mainParticleRadius and (particles[0][i], particles[1][i]) != (0,0)): # если частица основная и не находится в начале координат
            temp = [] # временный массив для значений phi

            # проходим по всем остальным вспомогательным частицам
            for j in range(len(particles[0])):
                
                cond = math.sqrt((particles[0][j] - particles[0][i])**2 + (particles[1][j] - particles[1][i])**2) # √((x — x0)² + (y — y0)²) ≤ r
                if (particles[2][j] == otherParticleRadius and cond <= circleR): # находим вспомогательную частицу и лежит ли точка (центры частиц) в окружности
                    
                    # перемещаем координаты относительно начала
                    particleLx = particles[0][j] - particles[0][i]
                    particleLy = particles[1][j] - particles[1][i] 
                    # using atan2 
                    radians = math.atan2(particleLy, particleLx) if math.atan2(particleLy, particleLx) >= 0 else math.atan2(particleLy, particleLx) + 2*math.pi
                    phi = math.degrees(radians)
                    
                    temp.append(phi)
            # output.append((particles[0][i], particles[1][i], bondOrientationalParameter(temp))) if len(temp) != 0 else output.append((particles[0][i], particles[1][i], 0.0))
            if (len(temp) != 0 and len(temp) >= 3): output.append((particles[0][i], particles[1][i], bondOrientationalParameter(temp)))

if (len(output) == 0):
    print("length error")
    sys.exit()
else:
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
    for val in output:
        # счетчик для квадратов
        count = 0
        for j in range(len(coordinatesX) - 1):
            for k in range(len(coordinatesY) - 1):
                if ((coordinatesX[j] <= val[0] < coordinatesX[j+1]) and (coordinatesY[k] <= val[1] < coordinatesY[k+1])):
                    # temp[count].append(val[2])
                    temp[count].append(val[2])
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
    with open(wwwroot + "\\" + "BOPcoordinates" + "\\" + "heatmapData.txt", 'w') as f:
        for count, value in enumerate(result[::-1]):
            f.write(re.sub(braces, '',str(value)) + "\n")

    print(visualPath + "\n" + heatmapPath)