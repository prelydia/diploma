import random
import math
import matplotlib.pyplot as plt
import numpy as np
import sys

num_particles = int(sys.argv[3]) # количество генерируемых частиц
low = 0 # генерация частиц от (по оси х)
hight = 100 # генерация частиц до (по оси х)
webroot = sys.argv[1]
folder = sys.argv[4] # папка, где будут сохраняться сгенерированные координаты
path = webroot + '\\' + folder + '\\' # путь к папке с файлами сайта
status = sys.argv[2] # статус частиц 

# метод проверки на коллизию
def collisionCheck(particles, x, y, r):
    for particle in particles:
        if math.sqrt((x - particle[0])**2 + (y - particle[1])**2) < r + particle[2]: # √((x — x0)² + (y — y0)²) < r + r0
            return True
    return False

# метод генерации координат
def generateRandomCoordinates(num_particles, low, hight):
    particles = [] # массив для хранения коодинат

    for _ in range(num_particles):
        # генерируем рандомные координаты
        x = random.uniform(low, hight)
        y = random.uniform(low, hight)
        r = random.randint(1, 2)
        
        # исключаем возможные коллизии
        while (collisionCheck(particles, x, y, r)):
            x = random.uniform(low, hight)
            y = random.uniform(low, hight)
            r = random.randint(1, 2)

        particles.append((x, y, r))

    return particles

def visualization(particles, hight):
    fig, ax = plt.subplots()
    ax.set_xlim(0, hight)
    ax.set_ylim(0, hight)

    for particle in particles:
        x, y, radius = particle
        if (status == 'S'): circle = plt.Circle((x, y), radius, color='b' if radius == 2 else 'r', fill=True) # красные - основные, синие - всторостепенные
        else: circle = plt.Circle((x, y), radius, color='r' if radius == 2 else 'b', fill=True)
        ax.add_patch(circle)

    plt.gca().set_aspect('equal')
    plt.show()

particles = generateRandomCoordinates(num_particles, low, hight)

# записываем координаты в файл
with open(path + 'chaoscoordinatesN=' + str(num_particles) + '.txt', 'w') as f:
    for particle in particles:
        f.write(str(particle[0]) + " " + str(particle[1]) + " " + str(particle[2]) + "\n")

print('chaoscoordinatesN=' + str(num_particles) + '.txt')
# visualization(particles, hight)