#include <iostream>
#include <sys/types.h>
#include <sys/socket.h>
#include <sys/un.h>
#include <unistd.h>
#include <thread>

#define SOCKET_PATH "socket/sock"

void read_thread(int clientSocket)
{
    // Read data from the client
    while (true)
    {
        char buffer[1024] = {0};
        int bytesRead = read(clientSocket, buffer, sizeof(buffer));
        if (strlen(buffer) == 0)
        {
            continue;
        }
        printf("Received message from client: %s\n", buffer);
        // float lat, lon, heading;
        // sscanf(buffer, "l:%f,%f&&h:%f&&s:50&&b:0", &lat, &lon, &heading);
        // static int id = 0;
        // printf("const char *response%d = {\"000000000000l%f,%f\"};\nsend(clientSocket, response%d, strlen(response%d), 0);\n", id, lat, lon, id, id);
        // id++;
        // printf("const char *response%d = {\"000000000000h%f\"};\nsend(clientSocket, response%d, strlen(response%d), 0);\nsleep(1);\n", id, heading, id, id);
        // id++;
    }
}

int main()
{
    int serverSocket, clientSocket;
    struct sockaddr_un serverAddress, clientAddress;
    socklen_t clientAddressLength = sizeof(clientAddress);
    char buffer[1024] = {0};

    // Create a socket
    if ((serverSocket = socket(AF_UNIX, SOCK_STREAM, 0)) == -1)
    {
        perror("Failed to create socket");
        return 1;
    }

    // Set server details
    serverAddress.sun_family = AF_UNIX;
    strncpy(serverAddress.sun_path, SOCKET_PATH, sizeof(serverAddress.sun_path) - 1);

    // Bind the socket to a specific path
    if (bind(serverSocket, (struct sockaddr *)&serverAddress, sizeof(serverAddress)) == -1)
    {
        perror("Failed to bind");
        return 1;
    }

    // Listen for incoming connections
    if (listen(serverSocket, 1) == -1)
    {
        perror("Failed to listen");
        return 1;
    }

    // Accept a client connection
    if ((clientSocket = accept(serverSocket, (struct sockaddr *)&clientAddress, &clientAddressLength)) == -1)
    {
        perror("Failed to accept");
        return 1;
    }

    // Read data from the client
    // int bytesRead = read(clientSocket, buffer, sizeof(buffer));
    // printf("Received message from client: %s\n", buffer);

    std::thread thread_object(read_thread, clientSocket);

    while (true)
    {
        const char *response0 = {"000000000000l2.330000,-10.300790"};
        send(clientSocket, response0, strlen(response0), 0);
        const char *response1 = {"000000000000h0"};
        send(clientSocket, response1, strlen(response1), 0);
        sleep(1);
        
        const char *response2 = {"000000000000l2.330000,-10.300790"};
        send(clientSocket, response2, strlen(response2), 0);
        const char *response3 = {"000000000000h0"};
        send(clientSocket, response3, strlen(response3), 0);
        sleep(1);
        const char *response4 = {"000000000000l2.330000,-10.090730"};
        send(clientSocket, response4, strlen(response4), 0);
        const char *response5 = {"000000000000h0"};
        send(clientSocket, response5, strlen(response5), 0);
        sleep(1);

        const char *response6 = {"000000000000l2.330000,-6.334980"};
        send(clientSocket, response6, strlen(response6), 0);
        const char *response7 = {"000000000000h0.000000"};
        send(clientSocket, response7, strlen(response7), 0);
        sleep(1);
        const char *response8 = {"000000000000l2.330000,-0.739020"};
        send(clientSocket, response8, strlen(response8), 0);
        const char *response9 = {"000000000000h0.000000"};
        send(clientSocket, response9, strlen(response9), 0);
        sleep(1);
        const char *response10 = {"000000000000l2.330000,4.204170"};
        send(clientSocket, response10, strlen(response10), 0);
        const char *response11 = {"000000000000h0.000000"};
        send(clientSocket, response11, strlen(response11), 0);
        sleep(1);
        const char *response12 = {"000000000000l2.330000,8.554540"};
        send(clientSocket, response12, strlen(response12), 0);
        const char *response13 = {"000000000000h0.000000"};
        send(clientSocket, response13, strlen(response13), 0);
        sleep(1);
        const char *response14 = {"000000000000l2.330000,12.427030"};
        send(clientSocket, response14, strlen(response14), 0);
        const char *response15 = {"000000000000h0.000000"};
        send(clientSocket, response15, strlen(response15), 0);
        sleep(1);
        const char *response16 = {"000000000000l2.330000,16.056219"};
        send(clientSocket, response16, strlen(response16), 0);
        const char *response17 = {"000000000000h0.000000"};
        send(clientSocket, response17, strlen(response17), 0);
        sleep(1);
        const char *response18 = {"000000000000l2.330000,19.215160"};
        send(clientSocket, response18, strlen(response18), 0);
        const char *response19 = {"000000000000h0.000000"};
        send(clientSocket, response19, strlen(response19), 0);
        sleep(1);
        const char *response20 = {"000000000000l2.330000,22.707069"};
        send(clientSocket, response20, strlen(response20), 0);
        const char *response21 = {"000000000000h0.000000"};
        send(clientSocket, response21, strlen(response21), 0);
        sleep(1);
        const char *response22 = {"000000000000l2.330460,27.433149"};
        send(clientSocket, response22, strlen(response22), 0);
        const char *response23 = {"000000000000h0.012650"};
        send(clientSocket, response23, strlen(response23), 0);
        sleep(1);
        const char *response24 = {"000000000000l2.478220,32.700081"};
        send(clientSocket, response24, strlen(response24), 0);
        const char *response25 = {"000000000000h3.996010"};
        send(clientSocket, response25, strlen(response25), 0);
        sleep(1);
        const char *response26 = {"000000000000l2.990730,39.104130"};
        send(clientSocket, response26, strlen(response26), 0);
        const char *response27 = {"000000000000h4.504540"};
        send(clientSocket, response27, strlen(response27), 0);
        sleep(1);
        const char *response28 = {"000000000000l1.609010,44.316818"};
        send(clientSocket, response28, strlen(response28), 0);
        const char *response29 = {"000000000000h317.594086"};
        send(clientSocket, response29, strlen(response29), 0);
        sleep(1);
        const char *response30 = {"000000000000l-1.768610,44.680340"};
        send(clientSocket, response30, strlen(response30), 0);
        const char *response31 = {"000000000000h252.925400"};
        send(clientSocket, response31, strlen(response31), 0);
        sleep(1);
        const char *response32 = {"000000000000l-3.281160,42.293671"};
        send(clientSocket, response32, strlen(response32), 0);
        const char *response33 = {"000000000000h200.118195"};
        send(clientSocket, response33, strlen(response33), 0);
        sleep(1);
        const char *response34 = {"000000000000l-2.869300,39.045261"};
        send(clientSocket, response34, strlen(response34), 0);
        const char *response35 = {"000000000000h172.921997"};
        send(clientSocket, response35, strlen(response35), 0);
        sleep(1);
        const char *response36 = {"000000000000l-3.285730,35.462341"};
        send(clientSocket, response36, strlen(response36), 0);
        const char *response37 = {"000000000000h187.759796"};
        send(clientSocket, response37, strlen(response37), 0);
        sleep(1);
        const char *response38 = {"000000000000l-3.884190,31.070299"};
        send(clientSocket, response38, strlen(response38), 0);
        const char *response39 = {"000000000000h187.779800"};
        send(clientSocket, response39, strlen(response39), 0);
        sleep(1);
        const char *response40 = {"000000000000l-4.835780,24.105890"};
        send(clientSocket, response40, strlen(response40), 0);
        const char *response41 = {"000000000000h187.780899"};
        send(clientSocket, response41, strlen(response41), 0);
        sleep(1);
        const char *response42 = {"000000000000l-5.828330,16.415649"};
        send(clientSocket, response42, strlen(response42), 0);
        const char *response43 = {"000000000000h186.979202"};
        send(clientSocket, response43, strlen(response43), 0);
        sleep(1);
        const char *response44 = {"000000000000l-5.957230,10.581350"};
        send(clientSocket, response44, strlen(response44), 0);
        const char *response45 = {"000000000000h165.295700"};
        send(clientSocket, response45, strlen(response45), 0);
        sleep(1);
        const char *response46 = {"000000000000l-3.611800,5.480150"};
        send(clientSocket, response46, strlen(response46), 0);
        const char *response47 = {"000000000000h163.710693"};
        send(clientSocket, response47, strlen(response47), 0);
        sleep(1);
        const char *response48 = {"000000000000l-4.278260,-2.465450"};
        send(clientSocket, response48, strlen(response48), 0);
        const char *response49 = {"000000000000h205.870300"};
        send(clientSocket, response49, strlen(response49), 0);
        sleep(1);
        const char *response50 = {"000000000000l-9.427950,-6.488440"};
        send(clientSocket, response50, strlen(response50), 0);
        const char *response51 = {"000000000000h260.330292"};
        send(clientSocket, response51, strlen(response51), 0);
        sleep(1);
        const char *response52 = {"000000000000l-13.810690,-4.396260"};
        send(clientSocket, response52, strlen(response52), 0);
        const char *response53 = {"000000000000h323.226990"};
        send(clientSocket, response53, strlen(response53), 0);
        sleep(1);
        const char *response54 = {"000000000000l-15.892220,1.849660"};
        send(clientSocket, response54, strlen(response54), 0);
        const char *response55 = {"000000000000h0.332530"};
        send(clientSocket, response55, strlen(response55), 0);
        sleep(1);
        const char *response56 = {"000000000000l-15.745170,7.528700"};
        send(clientSocket, response56, strlen(response56), 0);
        const char *response57 = {"000000000000h1.672530"};
        send(clientSocket, response57, strlen(response57), 0);
        sleep(1);
        const char *response58 = {"000000000000l-15.697990,12.165550"};
        send(clientSocket, response58, strlen(response58), 0);
        const char *response59 = {"000000000000h359.176605"};
        send(clientSocket, response59, strlen(response59), 0);
        sleep(1);
        const char *response60 = {"000000000000l-15.756380,16.228010"};
        send(clientSocket, response60, strlen(response60), 0);
        const char *response61 = {"000000000000h359.186096"};
        send(clientSocket, response61, strlen(response61), 0);
        sleep(1);
        const char *response62 = {"000000000000l-14.853950,19.153999"};
        send(clientSocket, response62, strlen(response62), 0);
        const char *response63 = {"000000000000h31.136459"};
        send(clientSocket, response63, strlen(response63), 0);
        sleep(1);
        const char *response64 = {"000000000000l-12.267780,20.558270"};
        send(clientSocket, response64, strlen(response64), 0);
        const char *response65 = {"000000000000h67.127060"};
        send(clientSocket, response65, strlen(response65), 0);
        sleep(1);
        const char *response66 = {"000000000000l-7.263510,20.907890"};
        send(clientSocket, response66, strlen(response66), 0);
        const char *response67 = {"000000000000h87.672417"};
        send(clientSocket, response67, strlen(response67), 0);
        sleep(1);
        const char *response68 = {"000000000000l-1.748890,21.114401"};
        send(clientSocket, response68, strlen(response68), 0);
        const char *response69 = {"000000000000h88.040977"};
        send(clientSocket, response69, strlen(response69), 0);
        sleep(1);
        const char *response70 = {"000000000000l2.915100,21.273930"};
        send(clientSocket, response70, strlen(response70), 0);
        const char *response71 = {"000000000000h88.040909"};
        send(clientSocket, response71, strlen(response71), 0);
        sleep(1);
        const char *response72 = {"000000000000l6.878090,21.571280"};
        send(clientSocket, response72, strlen(response72), 0);
        const char *response73 = {"000000000000h81.305496"};
        send(clientSocket, response73, strlen(response73), 0);
        sleep(1);
        const char *response74 = {"000000000000l9.826540,23.938030"};
        send(clientSocket, response74, strlen(response74), 0);
        const char *response75 = {"000000000000h29.993540"};
        send(clientSocket, response75, strlen(response75), 0);
        sleep(1);
        const char *response76 = {"000000000000l9.138380,28.219521"};
        send(clientSocket, response76, strlen(response76), 0);
        const char *response77 = {"000000000000h322.966095"};
        send(clientSocket, response77, strlen(response77), 0);
        sleep(1);
        const char *response78 = {"000000000000l4.811760,31.358841"};
        send(clientSocket, response78, strlen(response78), 0);
        const char *response79 = {"000000000000h306.935486"};
        send(clientSocket, response79, strlen(response79), 0);
        sleep(1);
        const char *response80 = {"000000000000l-0.109280,34.363510"};
        send(clientSocket, response80, strlen(response80), 0);
        const char *response81 = {"000000000000h284.584290"};
        send(clientSocket, response81, strlen(response81), 0);
        sleep(1);
        const char *response82 = {"000000000000l-4.732530,32.671902"};
        send(clientSocket, response82, strlen(response82), 0);
        const char *response83 = {"000000000000h237.697403"};
        send(clientSocket, response83, strlen(response83), 0);
        sleep(1);
        const char *response84 = {"000000000000l-8.916840,28.928699"};
        send(clientSocket, response84, strlen(response84), 0);
        const char *response85 = {"000000000000h209.361694"};
        send(clientSocket, response85, strlen(response85), 0);
        sleep(1);
        const char *response86 = {"000000000000l-9.272660,23.187540"};
        send(clientSocket, response86, strlen(response86), 0);
        const char *response87 = {"000000000000h177.871796"};
        send(clientSocket, response87, strlen(response87), 0);
        sleep(1);
        const char *response88 = {"000000000000l-8.671670,15.310660"};
        send(clientSocket, response88, strlen(response88), 0);
        const char *response89 = {"000000000000h171.481705"};
        send(clientSocket, response89, strlen(response89), 0);
        sleep(1);
        const char *response90 = {"000000000000l-7.404800,6.888660"};
        send(clientSocket, response90, strlen(response90), 0);
        const char *response91 = {"000000000000h171.393997"};
        send(clientSocket, response91, strlen(response91), 0);
        sleep(1);
        const char *response92 = {"000000000000l-4.329170,-0.050760"};
        send(clientSocket, response92, strlen(response92), 0);
        const char *response93 = {"000000000000h137.483505"};
        send(clientSocket, response93, strlen(response93), 0);
        sleep(1);
        const char *response94 = {"000000000000l1.704540,-5.837950"};
        send(clientSocket, response94, strlen(response94), 0);
        const char *response95 = {"000000000000h122.743301"};
        send(clientSocket, response95, strlen(response95), 0);
        sleep(1);
        const char *response96 = {"000000000000l10.551220,-10.339920"};
        send(clientSocket, response96, strlen(response96), 0);
        const char *response97 = {"000000000000h116.050697"};
        send(clientSocket, response97, strlen(response97), 0);
        sleep(1);
        const char *response98 = {"000000000000l19.849051,-14.884960"};
        send(clientSocket, response98, strlen(response98), 0);
        const char *response99 = {"000000000000h116.050598"};
        send(clientSocket, response99, strlen(response99), 0);
        sleep(1);
        const char *response100 = {"000000000000l30.479601,-18.900299"};
        send(clientSocket, response100, strlen(response100), 0);
        const char *response101 = {"000000000000h88.645950"};
        send(clientSocket, response101, strlen(response101), 0);
        sleep(1);
        const char *response102 = {"000000000000l39.003189,-17.947889"};
        send(clientSocket, response102, strlen(response102), 0);
        const char *response103 = {"000000000000h95.284286"};
        send(clientSocket, response103, strlen(response103), 0);
        sleep(1);
        const char *response104 = {"000000000000l47.643082,-21.442680"};
        send(clientSocket, response104, strlen(response104), 0);
        const char *response105 = {"000000000000h122.902802"};
        send(clientSocket, response105, strlen(response105), 0);
        sleep(1);
        const char *response106 = {"000000000000l53.741280,-27.303419"};
        send(clientSocket, response106, strlen(response106), 0);
        const char *response107 = {"000000000000h155.602295"};
        send(clientSocket, response107, strlen(response107), 0);
        sleep(1);
        const char *response108 = {"000000000000l55.991989,-34.389301"};
        send(clientSocket, response108, strlen(response108), 0);
        const char *response109 = {"000000000000h167.471802"};
        send(clientSocket, response109, strlen(response109), 0);
        sleep(1);
        const char *response110 = {"000000000000l55.399872,-41.997921"};
        send(clientSocket, response110, strlen(response110), 0);
        const char *response111 = {"000000000000h207.422104"};
        send(clientSocket, response111, strlen(response111), 0);
        sleep(1);
        const char *response112 = {"000000000000l51.921200,-47.742550"};
        send(clientSocket, response112, strlen(response112), 0);
        const char *response113 = {"000000000000h215.471207"};
        send(clientSocket, response113, strlen(response113), 0);
        sleep(1);
        const char *response114 = {"000000000000l48.271900,-52.987930"};
        send(clientSocket, response114, strlen(response114), 0);
        const char *response115 = {"000000000000h203.569107"};
        send(clientSocket, response115, strlen(response115), 0);
        sleep(1);
        const char *response116 = {"000000000000l48.397221,-58.693459"};
        send(clientSocket, response116, strlen(response116), 0);
        const char *response117 = {"000000000000h159.361099"};
        send(clientSocket, response117, strlen(response117), 0);
        sleep(1);
        const char *response118 = {"000000000000l48.969818,-64.582718"};
        send(clientSocket, response118, strlen(response118), 0);
        const char *response119 = {"000000000000h180.410706"};
        send(clientSocket, response119, strlen(response119), 0);
        sleep(1);
        const char *response120 = {"000000000000l46.440639,-71.331818"};
        send(clientSocket, response120, strlen(response120), 0);
        const char *response121 = {"000000000000h218.110306"};
        send(clientSocket, response121, strlen(response121), 0);
        sleep(1);
        const char *response122 = {"000000000000l40.442581,-74.142197"};
        send(clientSocket, response122, strlen(response122), 0);
        const char *response123 = {"000000000000h270.702087"};
        send(clientSocket, response123, strlen(response123), 0);
        sleep(1);
        const char *response124 = {"000000000000l34.615849,-73.353577"};
        send(clientSocket, response124, strlen(response124), 0);
        const char *response125 = {"000000000000h278.840515"};
        send(clientSocket, response125, strlen(response125), 0);
        sleep(1);
        const char *response126 = {"000000000000l29.831221,-71.674599"};
        send(clientSocket, response126, strlen(response126), 0);
        const char *response127 = {"000000000000h309.958801"};
        send(clientSocket, response127, strlen(response127), 0);
        sleep(1);
        const char *response128 = {"000000000000l29.304581,-68.324722"};
        send(clientSocket, response128, strlen(response128), 0);
        const char *response129 = {"000000000000h12.385030"};
        send(clientSocket, response129, strlen(response129), 0);
        sleep(1);
    }
    // Close the sockets
    close(clientSocket);
    close(serverSocket);

    // Remove the socket file
    unlink(SOCKET_PATH);

    return 0;
}
