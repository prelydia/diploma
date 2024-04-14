import random
from turtle import circle
import matplotlib.pyplot as plt
import sys

col = 6 # количество столбцов
row = 6 # количество строк
r = 1 # радиус малой частицы
R = 2 # радиус большой частицы
circleR = r + 2*R # радиус круга соседей первого порядка
particles = [] # массив координат
webroot = sys.argv[1]
# path = webroot + '\\BOPcoordinates\\' # путь к папке с файлами сайта
statusCheck = sys.argv[2] # флажок для смены статусов частиц
folder = sys.argv[4] # для хранения папки, куда будут сохраняться координаты
path = webroot + "\\" + folder + "\\" # путь

fig, ax = plt.subplots()
ax.set_xlim(-10, col * 5)
ax.set_ylim(-10, row * 5)

# проходим по столбцам для вспомогательных частиц
for i in range(col):
    xL = (circleR * i) # координата хL

    # проходим по строкам
    for j in range(row):
        yL = (circleR * j) # координата уL

        circleL = plt.Circle((xL, yL), R if statusCheck == "S" else r, color='b', fill=True)
        particles.append((xL, yL, R if statusCheck == "S" else r))
        ax.add_patch(circleL)

# проходим по столбцам для основных частиц
for i in range(col-1):
    prob = random.randint(1, 5) # вероятность 1/5
    xS = circleR/2 + (circleR * i) # координата xS

    # в определенных случаях сдвигаем частицу по оси Ох
    if (prob == 2): xS += r/2
    elif (prob == 4): xS -= r/2
    
    for j in range(row - 1):
        yS = circleR/2 + (circleR * j)         

        # в определенных случаях сдвигаем частицу по оси Оу
        if (prob == 1): yS += r/2
        elif (prob == 3): yS -= r/2  

        circleS = plt.Circle((xS, yS), r if statusCheck == "S" else R, color='r', fill=True)
        particles.append((xS, yS, r if statusCheck == "S" else R))
        ax.add_patch(circleS)
 
num_particles = len(particles) # количество частиц

# записываем координаты в файл
with open(path + 'ordercoordinatesN=' + str(num_particles) + '.txt', 'w') as f:
    for particle in particles:
        f.write(str(particle[0]) + " " + str(particle[1]) + " " + str(particle[2]) + "\n")

print('ordercoordinatesN=' + str(num_particles) + '.txt')
# plt.gca().set_aspect('equal')
# plt.show()