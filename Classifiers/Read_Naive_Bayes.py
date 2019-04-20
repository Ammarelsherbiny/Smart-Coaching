import pymysql
import numpy as np
import cv2
from sklearn.naive_bayes import GaussianNB



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


def convert1(arr, c, max):
    count = 0

    for i in range(c):
            x = float(arr[i])
            #x = x * 1000
            data[numberofvideo][count] = np.float32(x)
            count += 1
    diff = max - count
    for i in range(diff):
        x = 0
        n = count + i
        data[numberofvideo][n] = np.float32(x)

def convert2(arr, c, max):
    count = 0

    for i in range(c):
            x = float(arr[i])
            #x = x * 1000
            test[count] = np.float32(x)
            count += 1
    diff = max - count
    for i in range(diff):
        x = 0
        n = count + i
        test[n] = np.float32(x)

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
    print('The video number:', array[1], ' The length of video:', countlen)
    maxNumberOfPoint.append(int(countlen))

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
    print('The video number:', array[1], ' The length of video:', countlen)
    maxNumberOfPoint.append(int(countlen))


def getVideoType(id):
    sql = "SELECT * FROM `template` WHERE  VideoId = %s"
    adr = (id)
    db.execute(sql, adr)
    result = db.fetchall()
    if (result[0][2] == 'Correct'):
        return 0
    if (result[0][2] == 'Wrong-Back'):
        return 1
    if (result[0][2] == 'Wrong-Hand'):
        return 2
    if (result[0][2] == 'Right'):
        return 3
    if (result[0][2] == 'Back'):
        return 4
    if (result[0][2] == 'Knee'):
        return 5





def fillCheck(res, min,id):
    for i in range(len(res)):
        if (min == res[i][0]):
            sql = "UPDATE `checkvideo` SET Value = %s WHERE VideoId = %s"
            if (res[i][1] == 'Right'):
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


first = selectFirstVideoPoint()


allPoints = selectAllVideoPoint()
TvideoID = selectTepmlateVideo()


lists = [[] for _ in range(len(TvideoID))]


maxNumberOfPoint = []
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
    #print(Test[i][0])

print(maxNumberOfPoint)

maxNumber = maxNumberOfPoint.copy()
maxNumber.sort()
print(maxNumberOfPoint)
print(maxNumber)

maxNumber = maxNumber[-1]+1
print(maxNumber)


data = np.random.randint(0,1,(numberofvideo,18*maxNumber))



f = 0
for j in range(len(Tests)):
    res = []
    numberofvideo = 0
    responses = np.random.randint(0, 1, len(TvideoID))

    for i in range(len(lists)):
        convert1(lists[i],len(lists[i]),maxNumber)
        numberofvideo += 1
        responses[i] = getVideoType(TvideoID[i][1])
    test = np.random.randint(0, 1, (18 * maxNumber))
    convert2(Tests[j],len(Tests[j]),maxNumber)

    data = data.astype(np.float32)
    test = test.astype(np.float32)
    #print(responses)
    responses = np.float32(responses)

    newcomer = np.random.randint(0, 1, (1, maxNumber))
    for o in range(maxNumber):
        newcomer[0][o] = test[o].astype(np.float32)

    newdata = np.random.randint(0, 1, (len(TvideoID), maxNumber))
    for o in range(len(TvideoID)):
        for p in range(maxNumber):
            newdata[o][p] = data[o][p].astype(np.float32)

    newcomer = newcomer.astype(np.float32)
    newdata = newdata.astype(np.float32)

    gaunb = GaussianNB()
    gaunb = gaunb.fit(newdata, responses)
    results = gaunb.predict(newcomer)
    #print(prediction)
    f+=1
    #print(f)
    if (results == 3):
        #print(" this is the Correct movement\n")
        print("Right")
    if (results == 4):
        #print(" this is the Wrong-Back movement\n")
        print("Back")
    if (results == 5):
        #print(" this is the Wrong-Hand movement\n")
        print("Knee")




