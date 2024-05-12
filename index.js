RotateVertices = function(rotationAngle, vertices) {

    DegreesToRadians = (rotationAngle/180.0) * 3.14159265358797

    rotationMatrix = [Math.cos(DegreesToRadians), -Math.sin(DegreesToRadians),
                      Math.sin(DegreesToRadians),  Math.cos(DegreesToRadians)]

    return [
        vertices[0] * rotationMatrix[0] + vertices[1] * rotationMatrix[1],
        vertices[0] * rotationMatrix[2] + vertices[1] * rotationMatrix[3]
    ]
}

ProjectVerticesMinMax = function(vertices, axis) {

    var minimum =  1000000000000
    var maximum = -1000000000000

    for (var i = 0; i < vertices.length/2; i++) {

        var vertex = [
            vertices[i*2],
            vertices[i*2+1]
        ]
        
        let projection = Math.sqrt(vertex[0] * axis[0] + vertex[1] * axis[1])
        if (projection < minimum) minimum = projection
        if (projection > maximum) maximum = projection
    }

    return {
        'min': minimum,
        'max': maximum
    }
}

SeparatingAxisTheoremCollisionDetection = function(object1, object2) {


    for (var vert = 0; vert < object1.vertices.length; vert++) {
        
        let vertexA = [object1.vertices[vert][0], 
                       object1.vertices[vert][1]]

        let vertexB = [object1.vertices[(vert + 1) % object1.vertices.length][0], 
                       object1.vertices[(vert + 1) % object1.vertices.length][1]]

        edge = [
            vertexB[0] - vertexA[0],
            vertexB[1] - vertexA[1]
        ]
        axis = [-edge[1], egde[0]]

        minmaxA = ProjectVerticesMinMax(object1.vertices, axis)
        minmaxB = ProjectVerticesMinMax(object2.vertices, axis)

        if (minmaxA.min > minmaxB.max || minmaxB.min > minmaxA.max) return false
    }

    for (var vert = 0; vert < object2.vertices.length; vert++) {
        
        let vertexA = [object2.vertices[vert][0], 
                       object2.vertices[vert][1]]
                       
        let vertexB = [object2.vertices[(vert + 1) % object2.vertices.length][0], 
                       object2.vertices[(vert + 1) % object2.vertices.length][1]]

        edge = [
            vertexB[0] - vertexA[0],
            vertexB[1] - vertexA[1]
        ]
        axis = [-edge[1], egde[0]]

        minmaxA = ProjectVerticesMinMax(object1.vertices, axis)
        minmaxB = ProjectVerticesMinMax(object2.vertices, axis)

        if (minmaxA.min > minmaxB.max || minmaxB.min > minmaxA.max) return false
    }

    return true
}

CreateObject = function(vertices) {

    return {
        'vertices': vertices,
        'vao': 0,
        'vbo': 0
    }
}










defaultVertexShader = ``
defaultFragmentShader = ``

object1 = CreateObject([[0, 0], [0, 0], [0, 0]])