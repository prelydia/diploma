#!/usr/bin/python
import csv
import time
from time import sleep
import numpy as np
import argparse
import cv2
import datetime
import imutils
from PIL import Image
import matplotlib.pyplot as plt
import numpy as np
import sys


# if __name__ == '__main__':
#    def callback(*arg):
#        print (arg)

#cv2.namedWindow( "result" ) НЕ НУЖНО ОКНО ЗАКРЫТИЯ
#color_yellow = (0,10,255)
hsv_min = np.array((0,0, 120), np.uint8) # начальный цвет дапазона
hsv_max = np.array((255,255,255), np.uint8) # конечный цвет диапазона

def barplot(x_data, y_data, error_data, x_label="", y_label="", title=""):
    _, ax = plt.subplots()
    # Draw bars, position them in the center of the tick mark on the x-axis
    ax.bar(x_data, y_data, color = '#539caf', align = 'center')
    # Draw error bars to show standard deviation, set ls to 'none'
    # to remove line between points
    ax.errorbar(x_data, y_data, yerr = error_data, color = '#297083', ls = 'none', lw = 2, capthick = 2)
    ax.set_ylabel(y_label)
    ax.set_xlabel(x_label)
    ax.set_title(title)
    
while True:
    # TEST PATH
    ## 'C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\files\\Тест системы_i.ivanov\\new_help_i.ivanov.jpg'
    #fn = 'C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\files\\Тест системы_i.ivanov\\new_help_i.ivanov.jpg'

    fn = sys.argv[1]

    # ДРУГОЙ СПОСОБ ЧТЕНИЯ ИЗОБРАЖЕНИЯ ПО БАЙТАМ
    stream = open(fn, 'rb')
    bytes = bytearray(stream.read())
    array = np.asarray(bytes, dtype=np.uint8)
    bgrImage = cv2.imdecode(array, cv2.IMREAD_UNCHANGED)
    clone = bgrImage.copy()

    # ИСПОЛЬЗУЕМ ДРУГОЙ СПОСОБ
    # img = cv2.imread(fn)
    # clone = img.copy()

    #flag, img = cap.read()
    hsv = cv2.cvtColor(bgrImage, cv2.COLOR_BGR2HSV ) # меняем цветовую модель с BGR на HSV
    thresh = cv2.inRange(hsv, hsv_min, hsv_max)
    #thresh =cv2.imread ('D:\RYBAKOV\IMAGES\RING\samp222.jpg')
    thresh = cv2.GaussianBlur(thresh, (3,3), 0)
    #thresh = cv2.dilate(thresh, None, iterations=1)
    #thresh = cv2.medianBlur(thresh,3)
    #kernel = np.ones((3, 3), np.uint8)
    #thresh = cv2.erode(thresh, kernel)

    #cv2.imshow('result', thresh) # ПОКАЗ

    cv2.imwrite('C:/Users/l.khusainova/Desktop/testing/1/thresh.png', thresh) # СОХРАНЕНИЕ (МОЖНО НЕ СОХРАНЯТЬ DEFAULT)

    #cv2.imwrite('result.jpg', thresh)
    contours, hierarchy = cv2.findContours( thresh.copy(), cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)


    #cv2.drawContours( img, contours, -1, (255,0,0), 2, cv2.LINE_AA, hierarchy, 1 )
    total = 0
    l=[] # диаметры
    circles = None
    contour_list = []

    for cnt in contours:
        approx = cv2.approxPolyDP(cnt,0.01*cv2.arcLength(cnt,True),True)
        area = cv2.contourArea(cnt)
        if ((len(approx) > 5) & (5 < area < 200) ):
            contour_list.append(cnt)
            ellipse = cv2.fitEllipse(cnt)
            (x,y),(MA,ma),angle = cv2.fitEllipse(cnt)
            d = ma/2
            rr=MA/ma
            l.append(d)
            #print (d)
            if (1<d<30)&(rr > 0.1):
                cv2.ellipse(bgrImage,ellipse,(255,0,0),2)
                total += 1

        cv2.drawContours(bgrImage, contour_list, -1, (0,255,0), 1)



    #cv2.imshow('contours', img) # ПОКАЗ

    cv2.imwrite('C://Users//l.khusainova//source//repos//diploma//diploma//wwwroot//graphs//particles//circs.png', bgrImage)  # СОХРАНЕНИЕ

    ch = cv2.waitKey(5)
    #print ('number',total)
    #print ('radius',l)
    print('\\graphs\\particles\\circs.png', '\n', '\\graphs\\result.png', '\n', total, '\n', l)
    x = range (len(l))

    barplot (x,l,1,1,2,3)
    #plt.show () # ПОКАЗ

    plt.savefig('C://Users//l.khusainova//source//repos//diploma//diploma//wwwroot//graphs//result.png')

    with open('particlesData.txt', 'w') as f:
        f.write(str(len(l))) # пустые ??
        f.write("\n")
        f.write(str(l))
    
    # file.close()

    raise SystemExit
