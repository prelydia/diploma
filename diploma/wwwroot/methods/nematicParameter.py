from collections import defaultdict
import numpy as np
import matplotlib.pyplot as plt
import math
import sys
from pathlib import Path
import os

Nring = int(sys.argv[5]) # количество подобластей (количество колец)
webroot = sys.argv[6] # сетевая папка
autoBorders = sys.argv[7] # автоматическое определение границ
S4 = 0 # параметр тетратического порядка 
R = 1 # радиус окружности

graphFolderName = 'pics' # название папки для хранения выходных графиков

# метод автоматического определения границ расположения палочек
def bordersCheck(isp):

    tempX = [] 
    for j in range(isp):

        folder = str(j+1) + '.txt' # folder name
        file = open(path + folder) # открываем файл с испытанием

        # перебираем массив для определения min&max r
        for line in file:
            l = line.split('\t')
            tempX.append(float(l[2]))
    
    # Определяем границы расположения палочек
    r1 = round(np.min(tempX), 4)
    r2 = round(np.max(tempX), 4)

    return r1, r2

# метод расчета S4
def visualization(center, point, end):
    ### ОКРУЖНОСТЬ ###
    radius = np.linalg.norm(center - point) # радиус = длина вектора
    # Генерируем набор углов для построения окружности
    angles = np.linspace(0, 2 * np.pi, 50)

    # Вычисляем координаты точек окружности
    x = center[0] + radius * np.cos(angles)
    y = center[1] + radius * np.sin(angles)
    ###################

    ### КАСАТЕЛЬНАЯ ###
    m = -(point[0] - center[0]) / (point[1] - center[1]) # Угловой коэффициент для касательной

    xTang = np.linspace(point[0] - 0.00015, point[0] + 0.00015, 50) # набор координат х для уравнения касатльной
    yTang = m * (xTang - point[0]) + point[1] # Уравнение касательной y - y1 = m(x - x1) => y = m(x - x1) + y1
    ###################
    
    ### ГОТОВИМ КООРДИНАТЫ ДЛЯ РАСЧЕТОВ ###
    endTang = ([xTang[len(xTang)-1], yTang[len(yTang)-1]]) # координаты конца касательной

    a = - ((point[1] * (end[0] - point[0]) / (math.sqrt(point[0]**2 + point[1]**2)))) # первая часть числителя
    b = (point[0] * (end[1] - point[1])) / ( math.sqrt(point[0]**2 + point[1]**2)) # вторая часть числителя
    c = math.sqrt((end[0] - point[0])**2 + (end[1] - point[1])**2) # знаменатель
    cos = (a + b)/c # косинус дельта
    cos2a = (2*cos**2) - 1# (8 * cos**4) - (8 * cos**2) + 1 

    cosToEdge = math.acos(cos) # извлекаем угол
    radToDegree = math.degrees(cosToEdge) # из радиан в градусы

    # print(radToDegree)
    #######################################

    # Создаем график и отображаем точку и окружность
    # fig, ax = plt.subplots()
    # ax.plot(x, y,)
    # ax.plot(point[0], point[1], 'ro')
    # ax.plot(center[0], center[1], 'bo')
    # ax.plot(start[0], start[1], 'bo')
    # ax.plot(end[0], end[1], 'yo')
    # ax.plot(start[0], start[1], color='yellow')
    # ax.plot(xTang, yTang, 'red')
    # ax.plot([point[0], end[0]], [point[1], end[1]], 'b') # соединение центра и конца
    # ax.plot([start[0], point[0]], [start[1], point[1]], 'b') # соединение центра и конца
    # ax.set_aspect('equal')
    # ax.legend()

    # # plt.axis([0.704, 0.710, -0.0015, 0.0020]) # для увеличение масштаба графика
    
    # plt.show()
    return cos2a

# метод постоения графика SEM
def graph(FilePath, array, webroot):
    # newFolderPath = webroot + graphFolderName + "\\" # название папки, где будем хранить графики

    arrVariables = []
    S = []
    r = []
    indexes = []
    with open(FilePath, 'r') as f:
        for lines in f:
            l1 = lines[0:-1]#?????
            l = l1.split('	')#?????
            arrVariables.append(l)

    # ВЫБИРАЕМ ТРЕБУЕМЫЕ ПАРАМЕТРЫ
    for i in range(len(arrVariables)):
        if arrVariables[i][0] == 'r':
            for j in range(Nring):
                r.append(arrVariables[i][j+1])
        if arrVariables[i][0] == 'SrAver':
            for j in range(Nring):
                if (arrVariables[i][j+1] != "-"):
                    S.append(float(arrVariables[i][j+1]))
                    indexes.append(j)

    r = [float(r[ind]) for ind in indexes] # выбираем только те индексы, которые нужно отметить на графике 

    # РАСЧЕТ SEM
    count = 0 # используем счетчик, потому что входим в массив без range
    SEMarr = [] # массив для хранения ошибок
    ResultSum = 0
    SEM = 0
    for m in array:
        tempArr = [a for b in array[m] for a in b]
        tempVar = 0
        if (len(tempArr) == 0):
            tempArr.clear()
        else:
            # print(tempArr, "-", S[count])
            for item in tempArr:
                tempVar = (float(item) - float(S[count]))**2
                ResultSum += tempVar
                # print(float(item), "-", float(S[count]), " sqr = ", tempVar)
                SEM = math.sqrt( ResultSum / len(tempArr) )
            SEMarr.append(SEM)
            count += 1
            ResultSum = 0
            SEM = 0

    # ЗАПИСЫВАЕМ SEM В ФАЙЛ
    # with open(FilePath.rpartition('\\')[0] + FilePath.rpartition('\\')[1] + 'SEM' + folderName + '.txt', 'w') as SEMf:
    #     SEMf.write('r\t\tSEM\n')
    #     for i in range(len(r)):
    #         SEMf.write(str(r[i]) + "\t" + str(SEMarr[i]) + "\n")

    with open(path + "SEM.txt", "w") as SEMf:
        SEMf.write('r\t\tSEM\n')
        for i in range(len(r)):
            SEMf.write(str(r[i]) + "\t" + str(SEMarr[i]) + "\n")

    plt.clf()
    plt.errorbar(r, S, yerr=SEMarr, fmt='o', ecolor='red', capsize=5)
    plt.plot(r, S, 'o')
    # plt.title("Зависимость параметра тетратического порядка \n от количества колец")
    plt.xlabel("$\it{r}$")
    plt.ylabel("$\it{S}$")
    # Разреживание оси x
    plt.xticks(r[::2])

    os.mkdir(path + 'SrSEM')
    # plt.savefig(newFolderPath + SrfileName + '.svg')
    # plt.savefig(newFolderPath + SrfileName + '.png')
    # plt.savefig(newFolderPath + SrfileName + '.jpeg')
    # plt.savefig(newFolderPath + SrfileName + '.eps')
    # plt.savefig(newFolderPath + SrfileName + '.pdf')

    # for web system
    plt.savefig(path + 'SrSEM\\SrSEM.svg')
    plt.savefig(path + 'SrSEM\\SrSEM.png')
    plt.savefig(path + 'SrSEM\\SrSEM.jpeg')
    plt.savefig(path + 'SrSEM\\SrSEM.eps')
    plt.savefig(path + 'SrSEM\\SrSEM.pdf')

path = sys.argv[1] + "\\" # путь к разархивированным файлам
SEM =  sys.argv[2] # строить график или нет (gets true or false)

folderName = path.split("\\")[-2] # считываем название текущей папки
rSfileName = "rS.txt" # название выходного текстового файла
SrfileName = "SrSEM" + folderName # название выходного графика

folder = Path(path)
isp = len(list(folder.iterdir())) # узнать, сколько файлов в папке, - это будет количество испытаний
lineLen = 8.7 # длина одного столбца (для полосы в файле)
Np = 0 # количество частиц

r1, r2 = bordersCheck(isp) if (autoBorders == "true") else (float(sys.argv[3]), float(sys.argv[4])) # границы определяются либо автоматически, либо через ввод

Length = r2 - r1 # длина заданного промежутка
w = Length / Nring # ширина кольца

rArr = [str(round(r1 + (i*w), 4)) for i,x in enumerate(list(range(Nring)))] # r = i*w
columnAver = defaultdict(list) # для расчета среднено по столбцам (r)

# with open(path + rSfileName, 'w') as f:
with open(rSfileName, 'w') as f:
    f.write("r\t" + "\t".join(rArr) + "\n") 

    for j in range(isp):
        folder = str(j+1) + '.txt' # folder name

        file = open(path + folder) # открываем файл с испытанием
        temp = defaultdict(list)

        FileFullPath = os.path.join(path, folder)

        line_count = sum(1 for line in open(FileFullPath)) # количество строк в файле (т.е. количество частиц PARTICLE_NUM)

        # ПРОВЕРКА НА КОЛИЧЕСТВО ЧАСТИЦ В ФАЙЛЕ
        if (line_count != Np and Np != 0):
            print("error")
            # break
            sys.exit(1)

        Np = line_count # меняем количество строк в файле на данном испытании

        for line in file:
            l = line.split('\t')

            # формируем вектор конца палочки
            x1, y1 = float(l[0]), float(l[1]) # начало палочки
            x2, y2 = float(l[2]), float(l[3]) # середина палочки
            x3, y3 = float(l[4]), float(l[5]) # конец палочки

            center = ([x2, y2])
            start = ([x1, y1])
            end = ([x3, y3])

            r = math.sqrt((x2)**2 + (y2)**2)   

            # в зависимости от условия считаем значение S
            for i in range(Nring):
                if (r1 + (i*w) <= r < r1 + w+(i*w)):
                    temp[i].append(visualization(np.array([0, 0]), center, end))

            # считаем средние значения по кольцам
            dictAver = defaultdict(list)
            for n in range(Nring):
                if (len(temp[n]) != 0): # в случае, когда палочки не попали в кольцо
                    dictAver[n].append(np.mean(temp[n]))

        # формируем строки для записи в файл
        tempStr = "" # временная строка для создания строк испытаний
        for ring in range(Nring):
            temp = "-" if len(dictAver[ring]) == 0 else str(round(dictAver[ring][0], 2))
            tempStr += temp + "\t"

        for jk in range(Nring):
            columnAver[jk].append(dictAver[jk])

        newLine = "S"+ str(j+1) + "\t" + tempStr + "\n"
        f.write(newLine) # записываем строки испытаний в файл
        tempStr = "" # очищаем строку

# расчитываем среднее по столбцам (т.е. по r)
SrAver = ["SrAver"]
for m in columnAver:
    # print(list(map(int, columnAver[m])))
    tempArr = [a for b in columnAver[m] for a in b]
    temp = "-" if len(tempArr) == 0 else round(np.mean(tempArr),2)
    SrAver.append(temp)

# with open(path + rSfileName, 'a') as f:
with open(rSfileName, 'a') as f:
    f.write("_" * int(Nring * lineLen) + "\n")

    for k in SrAver:
        f.write(str(k) + "\t")

# сохраняем индексы тех радиусов, в которые попали палочки
indexes = []
for i,x in enumerate(SrAver):
    if isinstance(x, (int, float)):
        indexes.append(i)

# теперь отображаем на графике только эти индексы (здесь строится просто график S4/r)
plt.plot([float(rArr[ind-1]) for ind in indexes], [x for x in SrAver if isinstance(x, (int, float))], 'o')
# plt.title("Зависимость параметра тетратического порядка \n от количества колец")
plt.xlabel("$\it{r}$")
plt.ylabel("$\it{S}$")
# print([float(rArr[ind-1]) for ind in indexes][::2])

# Разреживание оси x
plt.xticks([float(rArr[ind-1]) for ind in indexes][::2])

os.mkdir(path + "Sr")
plt.savefig(path + 'Sr\\Sr.svg')
plt.savefig(path + 'Sr\\Sr.jpeg')
plt.savefig(path + 'Sr\\Sr.png')
plt.savefig(path + 'Sr\\Sr.eps')
plt.savefig(path + 'Sr\\Sr.pdf')

print("\\" + path.split("\\")[-2] + '\\Sr\\Sr.svg')

# если выбрано построение графика SEM
if (SEM == 'true'):
    # graph(path + "rS.txt", columnAver)
    # graph(path + rSfileName, columnAver, webroot)
    graph(rSfileName, columnAver, webroot)

    print("\\" + path.split("\\")[-2] + '\\SrSEM\\SrSEM.svg')

print("Done")