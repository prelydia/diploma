import sys
import os
from pathlib import Path
import numpy as np
import math
import matplotlib.pyplot as plt

targetFolder = sys.argv[1]
# targetFolder = # 'C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\2oypwcjw2zm\\'
checkSEM = sys.argv[2] # 'true'

# СЧИТАЕМ КОЛИЧЕСТВО ПРОВЕДЕННЫХ ИСПЫТАНИЙ
count = 0
listOfDirs = []

X = 0 # индекс элемента массива с координатой Х
Y = 1 # индекс элемента массива с координатой У
Z = 2 # индекс элемента массива с координатой Z
L = 0 # характерный размер (радиус основания) CHARACTER_SIZE
Np = 0 # количество частиц PARTICLE_NUM
dt = 0 # временной шаг TIMESTEP
period = 0 # количество шагов в одном кадре STEPNUM

arrParticles0 = []
result = []
arrVariables = []
arrTime = []

Np = 0 # КОЛИЧЕСТВО ЧАСТИЦ PARTICLE_NUM
L = 0.001

for d in os.listdir(targetFolder):
    if os.path.isdir(os.path.join(targetFolder, d)):
        listOfDirs.append(d)

count = len(listOfDirs) # количество испытаний (т.е. количество разархивированных папок)

for i in range(count):
    folder = targetFolder + "\\" + str(i + 1) # путь с папкой испытания

    # считываем начальные координаты
    filename = "0.txt"
    FileFullPath = os.path.join(folder, filename)

    line_count = sum(1 for line in open(FileFullPath)) # количество строк в файле (т.е. количество частиц PARTICLE_NUM)

    ##print(FileFullPath, "||", line_count, "--", Np)

    # ПРОВЕРКА НА ОДИНАКОВОЕ КОЛИЧЕСТВО СТРОК В КАЖДОМ ФАЙЛЕ ИСПЫТАНИЙ
    if (line_count != Np and Np != 0):
        print("error")
        break

    Np = line_count

    with open(FileFullPath, 'r') as f:
        for num, lines in enumerate(f):
            if (num != 0):
                l = lines[0:-1].split(' ')

                # ДЛЯ ИСКЛЮЧЕНИЯ ОШИБКИ В СЛУЧАЕ ВЫБОРА РАЗМЕРНОСТИ, ОТЛИЧНОЙ ОТ 3D
                try:
                    arrParticles0.append([l[0], l[1], str(0)])
                except IndexError:
                    arrParticles0.append([l[0], l[1], str(0)])
                continue

    f.close()

    arrMSD = []

    # СЧИТАЕМ КОЛИЧЕСТВО ФАЙЛОВ В ПАПКЕ (Т.Е. КОЛИЧЕСТВО TIMESTEPNUM)
    files_count = len(list(Path(folder).iterdir()))

    for j in range(1, files_count):
        filename = str(j) + '.txt'

        FileFullPath = os.path.join(folder, filename)

        arrParticles = []

        line_count = sum(1 for line in open(FileFullPath))  # количество строк в файле (т.е. количество частиц PARTICLE_NUM)

        # ПРОВЕРКА НА ОДИНАКОВОЕ КОЛИЧЕСТВО СТРОК В КАЖДОМ ФАЙЛЕ ИСПЫТАНИЙ
        if (line_count != Np and Np != 0):
            print("error")
            raise SystemExit
            # break

        Np = line_count

        with open(FileFullPath, 'r') as f:
            for num, lines in enumerate(f):

                if (num == 0):
                    arrTime.append(int(lines))
                else:
                    l = lines[0:-1].split(' ')

                    # ДЛЯ ИСКЛЮЧЕНИЯ ОШИБКИ В СЛУЧАЕ ВЫБОРА РАЗМЕРНОСТИ, ОТЛИЧНОЙ ОТ 3D
                    try:
                        arrParticles.append([l[0], l[1], str(0)])
                    except IndexError:
                        arrParticles.append([l[0], l[1], str(0)])
                    continue
            f.close()
            # подсчет MSD
            msdNow = 0

            for i in range(len(arrParticles)):
                # ЧАСТНЫЙ СЛУЧАЙ
                # msdNow += ((float(arrParticles[i][X]) * L - float(arrParticles0[i][X]) * L) ** 2 + (
                #         float(arrParticles[i][Y]) * L - float(arrParticles0[i][Y]) * L) ** 2)

                # ОБЩИЙ СЛУЧАЙ
                msdNow += (float(arrParticles[i][X]) - float(arrParticles0[i][X])) ** 2 + \
                          (float(arrParticles[i][Y]) - float(arrParticles0[i][Y])) ** 2 + \
                          (float(arrParticles[i][Z]) - float(arrParticles0[i][Z])) ** 2

            msdNow = msdNow / Np

            arrMSD.append(msdNow)
    result.append(arrMSD)

    arrParticles0.clear()

arrTime = list(set(arrTime))

# file=open('testScriptOutput.txt', 'w')
# # file.write("Time \t\t part_1 \t\t part_2 \t\t part_3 \t\t part_4 \t\t part_5 \t\t part_6 \n")
# for i in range(len(arrTime)):
#     file.write(str(round(arrTime[i], 4)) + "\t" + str(result[0][i]) + "\t" + str(result[1][i]) + "\t" + str(result[2][i]) + "\t" + str(result[3][i]) + "\t" + str(result[4][i]) +
#                "\t" + str(result[5][i]) + "\n")
# file.close()

file = open("MSD.txt", "w")
file.write("Time \t\t ")
for i in range(count):
    file.write("part_" + str(i+1) + " \t\t ")
file.write("\n")

for i in range(len(arrTime)):
    file.write(str(round(arrTime[i], 4)) + "\t" + "\t".join([str(result[n][i]) for n in range(len(result))]) + "\n")
file.close()

arrForAver = []  # создаем массив, где будем хранить все значения построчно
arrAver = []

# СЧИТЫВАЕМ ЗНАЧЕНИЯ ПО СТРОКАМ (В МАССИВЕ ОНИ ПО СТОЛБАМ)
# 0 - 0, 1 - 0, 2 - 0 (first: len result[i], second: len result)
# 0 - 1, 1 - 1, 2 - 1
index = 0
for j in range(len(arrTime)):
    arrTemp = []
    for i in range(len(result)):
        arrTemp.append(result[i][j])
        # arrForAver[index].append(result[i][j])
    arrForAver.append(arrTemp)

# НАХОДИМ СРЕДНИЕ ЗНАЧЕНИЯ ПО ВЫСЧИТАННЫМ MSD
for i in arrForAver:
    arrAver.append(np.mean(i))

# СОХРАНЕНИЯ ФАЙЛА С ОШИБКАМИ
with open('AverageMSD.txt', 'w') as f:
    f.write("Time \t\t MSD \n")
    for i in range(len(arrTime)):
        f.write(str(arrTime[i]) + "\t" + str(arrAver[i]) + "\n")
    f.close()

plt.plot(arrTime, arrAver, "o")
plt.xlabel("Time, s")
plt.ylabel("Averaage MSD, m^2")
plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\MSD\\MSD.svg')
plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\MSD\\MSD.png')
plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\MSD\\MSD.jpeg')
plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\MSD\\MSD.eps')
plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\MSD\\MSD.pdf')
print('\\MSD\\MSD.svg')


# ЕСЛИ ТРЕБУЕТСЯ РАССЧИТАТЬ SEM И построить график
if (checkSEM == 'true'):

    # В ЦИКЛЕ РАССЧИТЫВАЕМ ОШИБКИ
    arrSEM = []
    SEM = 0
    for i in range(len(arrForAver)):
        # print("Часть ", i+1, "|| SEM=", SEM)
        for j in range(len(arrForAver[i])):
            SEM += (float(arrForAver[i][j]) - float(arrAver[i]))**2
            # print(float(arrMSD[i][j]), "-", float(arrAver[i]), "=", float(arrMSD[i][j]) - float(arrAver[i]))
            # print("Квадрат этого числа равен: ", (float(arrMSD[i][j]) - float(arrAver[i]))**2)
        SEM = math.sqrt(SEM / count) # делим на кол-во испытаний
        arrSEM.append(SEM)
        SEM = 0

    # СОХРАНЕНИЯ ФАЙЛА С ОШИБКАМИ
    with open('SEM.txt', 'w') as f:
        f.write("Time \t\t SEM \n")
        for i in range(len(arrTime)):
            f.write(str(arrTime[i]) + "\t" + str(arrSEM[i]) + "\n")
        f.close()

    plt.clf()
    # СТРОИМ ГРАФИК
    plt.errorbar(arrTime, arrAver, yerr=arrSEM, fmt='o', ecolor='red', capsize=5)
    plt.xlabel("Time, s")
    plt.ylabel("Average MSD, m^2")
    plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\SEM\\SEM.svg')
    plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\SEM\\SEM.png')
    plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\SEM\\SEM.jpeg')
    plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\SEM\\SEM.eps')
    plt.savefig('C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\SEM\\SEM.pdf')
    # plt.savefig(averPath + '\\20ispMSD1000')
    # plt.show()
    # СОХРАНЯЕМ ГРАФИК В ПАПКУ ПРОЕКТА
    print('\\SEM\\SEM.svg') # отправляем в программу ссылку на сохраненный график