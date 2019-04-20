import pymysql
import numpy as np
from fastdtw import fastdtw
from scipy.spatial.distance import euclidean

def convert(arr, c):
    #data = np.random.randint(0, 1, (c,18))
    count = 0
    newc =int( c / 18)
    data = np.random.randint(0, 1, (newc,18))
    for i in range(newc):
        for j in range(18):
            x = arr[count]
            data[i][j] = x
            count += 1
    return data

def read (file):
    c = 0
    arr =[]
    with open(file) as myfile:
        for line in myfile:
            arr.append(line.split(','))
            c = c + 1
    c = c - 1
    return arr,c

def convert1(arr, c):
    count = 0
    data = np.random.randint(0, 1, (c-1,18))
    for i in range(c-1):
        for j in range(18):
            x = float(arr[i][j])
            x = x * 1000
            data[i][j] = x
            count += 1
    return data

def selectFirstVideoPoint():
    db.execute("SELECT * FROM `videopoint`")
    firstresult = db.fetchone()
    return firstresult

def selectAllVideoPoint():
    db.execute("SELECT * FROM `videopoint`")
    myresult = db.fetchall()
    return myresult

def selectTepmlateVideo():
    db.execute("SELECT * FROM `template`")
    myresult = db.fetchall()
    return myresult

def selectTestVideo():
    db.execute("SELECT * FROM `checkvideo`")
    myresult = db.fetchall()
    return myresult

def getVideo(id):
    sql = "SELECT * FROM `videopoint` WHERE VideoId = %s"
    adr = (id)
    db.execute(sql, adr)
    result = db.fetchall()
    counter =3
    countlen = 0
    for array in result:
        for i in range(3):
            if (i == 0):
                o = array[3] * 1000
                x = int(o)
                lists[numberofvideo].append(x)
            if (i == 1):
                o = array[4] * 1000
                y = int(o)
                lists[numberofvideo].append(y)
            if (i == 2):
                o = array[5] * 1000
                z = int(o)
                lists[numberofvideo].append(z)
        #print('Length:',counter,' The video number:',array[1],'the joint number:', array[2], ', the X-axis:', x, ', the Y-axis:', y, ', the Z-axis:', z)
        counter += 3
        countlen += 1 / 6
    #print('The video number:', array[1], ' The length of video:', countlen)

def getTestVideo(id):
    sql = "SELECT * FROM `videopoint` WHERE VideoId = %s"
    adr = (id)
    db.execute(sql, adr)
    result = db.fetchall()
    counter =3
    countlen = 0
    for array in result:
        for i in range(3):
            if (i == 0):
                o = array[3] * 1000
                x = int(o)
                #x = array[3]
                Tests[numberofTestvideo].append(x)
            if (i == 1):
                o = array[4] * 1000
                y = int(o)
                #y = array[4]
                Tests[numberofTestvideo].append(y)
            if (i == 2):
                o = array[5] * 1000
                z = int(o)
                #z = array[5]
                Tests[numberofTestvideo].append(z)
        #print('Length:',counter,' The video number:',array[1],'the joint number:', array[2], ', the X-axis:', x, ', the Y-axis:', y, ', the Z-axis:', z)
        counter += 3
        countlen += 1/6
    #print('The video number:', array[1], ' The length of video:', countlen)

def fastDTW(test,f1):
    distance, path = fastdtw(test, f1, dist=euclidean)
    print(distance)
    return(distance)

def checkMin(list):
    minimum = list[0][0]

    for i in range(len(list)):
        if list[i][0] <= minimum:
            minimum = list[i][0]
    #print('ahuuu',minimum)
    return minimum

def fillCheck(res, min,id):
    for i in range(len(res)):
        if (min == res[i][0]):
            sql = "UPDATE `checkvideo` SET Value = %s WHERE VideoId = %s"
            if (res[i][1] == 'Correct'):
                value ='Correct'
                val = (value,id)
                db.execute(sql, val)
                conn.commit()
            if (res[i][1] == 'Wrong-Back'):
                value = 'Wrong-Back'
                val = (value, id)
                db.execute(sql, val)
                conn.commit()
            if (res[i][1] == 'Wrong-Hand'):
                value = 'Wrong-Hand'
                val = (value, id)
                db.execute(sql, val)
                conn.commit()
            if (res[i][1] == 'Knee'):
                value = 'Knee'
                val = (value, id)
                db.execute(sql, val)
                conn.commit()
            if (res[i][1] == 'Right'):
                value = 'Right'
                val = (value, id)
                db.execute(sql, val)
                conn.commit()
            if (res[i][1] == 'Back'):
                value = 'Back'
                val = (value, id)
                db.execute(sql, val)
                conn.commit()


conn=pymysql.connect(host='localhost',user='root',password='',db='gpcenter')

db=conn.cursor()


#first = selectFirstVideoPoint()


#allPoints = selectAllVideoPoint()
TvideoID = selectTepmlateVideo()


lists = [[] for _ in range(len(TvideoID))]

numberofvideo =0
for i in range(len(TvideoID)):
    getVideo(TvideoID[i][1])
    numberofvideo +=1

#### Testing:
Test = selectTestVideo()



Tests = [[] for _ in range(len(Test))]

numberofTestvideo =0
for i in range(len(Test)):
    getTestVideo(Test[i][1])
    numberofTestvideo +=1
    #print(Test[i][1])


for j in range(len(Tests)):
    res = []
    for i in range(len(lists)):
        f1 = convert(lists[i],len(lists[i]))
        test = convert(Tests[j],len(Tests[j]))

        distance = fastDTW(test, f1)
        #print(distance)
        res.append([distance, str(TvideoID[i][2])])
    #print(checkMin(res))
    check=checkMin(res)
    fillCheck(res,check,Test[j][1])


print(len(TvideoID))
print(len(Test))