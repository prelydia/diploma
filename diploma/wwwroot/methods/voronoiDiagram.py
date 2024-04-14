# импорт библиотек

import matplotlib.pyplot as plt
from scipy.spatial import Voronoi, voronoi_plot_2d
from matplotlib import axes
import sys
# points = [[1,2],[2,2],[3,3],[1,3],[2,1],[1,1]]


# объявление переменных и массивов
variables = [] # массив переменных
points = [] # массив координат для диаграммы Вороного
points_for_dots = [] # массив координат и радиусов для отрисовки частиц
x_up_border = 1.0
x_bot_border = 0.98
y_up_border = 0.05
y_bot_border = 0.03
x = 0
y = 1
r = 2
rp1 = 0
rp2 = 0

path = sys.argv[1] + "\\" # путь 

CfgFilename = '\\'.join(path.split("\\")[:-2]) + "\\" + sys.argv[2] # 'C:\\Users\\l.khusainova\\Desktop\\тест\\log.txt' # конфигурационный файл
Filename = '\\'.join(path.split("\\")[:-2]) + "\\" + sys.argv[3] # 'C:\\Users\\l.khusainova\\Desktop\\тест\\1570000.txt' # файл с координатами частиц

#CfgFilename = 'cfg.txt'
#Filename = '100.txt'

# чтение переменных и конф файла
with open(CfgFilename, 'r') as f:
    for lines in f:
        l1 = lines[0:-1]
        l = l1.split(' = ')
        variables.append(l)
    f.close()
# обработка и присвоение переменных из массива переменных
for i in range(len(variables)):
    if variables[i][0] == 'Number of particles':
        Np = float(variables[i][1])
    if variables[i][0] == 'radius of particles_1':
        rp1 = float(variables[i][1])
        dp1 = 2 * rp1
    if variables[i][0] == 'radius of particles_2':
        rp2 = float(variables[i][1])
        dp2 = 2 * rp2
    if variables[i][0] == 'L':
        scaleLen = float(variables[i][1])

Rd = 1.375e-3 # радиус капли
scaleLen=Rd # коэф обезразмеривания
# чтение координат из файла для отрисовки частиц
with open(Filename, 'r') as f:
    for lines in f:
        l1 = lines[0:-3]
        l = l1.split('\t')
        l[x] = float(l[x])
        l[y] = float(l[y])
        l[r] = float(l[r])
        # определение области обработки
        if l[x]>x_bot_border and l[x]<x_up_border and l[y]>y_bot_border and l[y]<y_up_border:# задать границы отдельными переменными ?
            points_for_dots.append(l)
    f.close()
# чтение координат из файла для диаграммы Вороного
with open(Filename, 'r') as f:
    for lines in f:
        l1 = lines[0:-3]
        l2 = l1.split('\t')
        l2[x] = float(l2[x])
        l2[y] = float(l2[y])
        l = []
        l.append(l2[x])
        l.append(l2[y])
        # определение области обработки
        if l[x]>x_bot_border and l[x]<x_up_border and l[y]>y_bot_border and l[y]<y_up_border:
            points.append(l)
    f.close()

# построение диаграммы Вороного
vor = Voronoi(points)
fig = voronoi_plot_2d(vor, show_vertices=0, show_points=0, line_colors='black', line_width=0.7, point_size=1)
# размер картинки
fig.set_size_inches(7, 10)

# Отрисовка частиц
for i in range(len(points_for_dots)):
    # проверка радиуса частицы
    if points_for_dots[i][r]<(rp1+rp1*0.01) and points_for_dots[i][r]>(rp1-rp1*0.01):
        circle1 = plt.Circle((points_for_dots[i][x], points_for_dots[i][y]), rp1/scaleLen, color='blue')
        plt.gcf().gca().add_artist(circle1)
    if points_for_dots[i][r]<(rp2+rp2*0.01) and points_for_dots[i][r]>(rp2-rp2*0.01):
        circle2 = plt.Circle((points_for_dots[i][x], points_for_dots[i][y]), rp2/scaleLen, color='brown')
        plt.gcf().gca().add_artist(circle2)

# сохранение диаграммы
fig.savefig(path + 'graphs\\diagvor.svg')
fig.savefig(path + 'graphs\\diagvor.png')
fig.savefig(path + 'graphs\\diagvor.jpeg')
fig.savefig(path + 'graphs\\diagvor.pdf')
fig.savefig(path + 'graphs\\diagvor.eps')
# отображение на экране
# plt.show()

print('\\graphs\\diagvor.svg')
