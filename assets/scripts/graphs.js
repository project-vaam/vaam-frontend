﻿var cy = null

var currentProcess = {
    nodeTimes: [],
    relationTimes: [],
    nodesMaxTime: '',
    edgesMaxTime: '',
};


function generateConformance(process) {

    console.log(process);
    renderConformanceGraph();
}


function generateGraph(process) {

    if (process.data.info == "Inductive Mining") {

        if (process.isPerformance === "True") {
            renderPerformanceGraph();
        }
    }
    else {
        if (process.isPerformance === "True") {

            //nodes timers
            let nodeTimes = [];
            for (let i = 0; i < process.data.statistics.nodes.length; i++) {
                let meanDuration = process.data.statistics.nodes[i].meanDuration.days + '.' + process.data.statistics.nodes[i].meanDuration.hours + ':' +
                    process.data.statistics.nodes[i].meanDuration.minutes + ':' + process.data.statistics.nodes[i].meanDuration.seconds + '.' + process.data.statistics.nodes[i].meanDuration.millis;

                nodeTimes[i] = moment.duration(meanDuration).asMinutes();

            }
            currentProcess.nodeTimes = nodeTimes;
            //console.log("relationTimes:" + nodeTimes)

            //relations timers
            let relationTimes = [];
            for (let i = 0; i < process.data.statistics.relations.length; i++) {
                let relationTimesTemp = [];
                for (let j = 0; j < process.data.statistics.relations[i].to.length; j++) {
                    let meanDuration = process.data.statistics.relations[i].to[j].meanDuration.days + '.' + process.data.statistics.relations[i].to[j].meanDuration.hours + ':' +
                        process.data.statistics.relations[i].to[j].meanDuration.minutes + ':' + process.data.statistics.relations[i].to[j].meanDuration.seconds + '.' + process.data.statistics.relations[i].to[j].meanDuration.millis;
                    relationTimesTemp[j] = moment.duration(meanDuration).asMinutes();
                }
                relationTimes[i] = relationTimesTemp;
            }

            currentProcess.relationTimes = relationTimes;

            console.log("relationTimes:" + relationTimes)
            console.log("nodeTimes:" + nodeTimes)

            renderPerformanceGraph();
        } else {

            console.log("frequency");
            renderFrequencyGraph();
        }
    }


}


/*-----------------Performance Diagram*-------------------*/


function renderPerformanceGraph() {
    cy = cytoscape(
        {
            wheelSensitivity: 0.1,
            minZoom: 0.1,
            maxZoom: 1.5,
            zoom: 0.5,
            container: document.getElementById('cy'), // container to render in   

            style: [
                {
                    //Nodes styles
                    selector: 'node[type=0]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#fff0d9",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=1]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#ffce89",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=2]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#ff9457",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=3]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#f25831",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=4]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#be1300",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#FFFFFF"
                    }
                },
                {
                    //process start node
                    selector: 'node[type=20]',
                    style: {
                        "shape": 'ellipse',
                        "background-color": "#3f3f3f",
                        "border-width": 4,
                        "border-color": "#131313",
                        'width': '50',
                        "height": "50",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "text-wrap": "wrap",
                        "text-max-width": "1000px",
                        "color": "#FF2222"
                    }
                },

                //Edges styles
                {
                    selector: 'edge[type=0]',
                    style: {
                        'width': 3,
                        'curve-style': 'bezier',
                        "content": "data(label)",
                        "line-color": "#787679",
                        'target-arrow-color': '#787679',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=1]',
                    style: {
                        'width': 5,
                        'curve-style': 'bezier',
                        "content": "data(label)",
                        "line-color": "#817073",
                        'target-arrow-color': '#817073',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=2]',
                    style: {
                        'width': 7,
                        'curve-style': 'bezier',
                        "content": "data(label)",
                        "line-color": "#817073",
                        'target-arrow-color': '#817073',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=3]',
                    style: {
                        'width': 9,
                        'curve-style': 'bezier',
                        "content": "data(label)",
                        "line-color": "#8f534d",
                        'target-arrow-color': '#8f534d',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=4]',
                    style: {
                        'width': 13,
                        'curve-style': 'bezier',
                        "content": "data(label)",
                        "line-color": "#b91200",
                        'target-arrow-color': '#b91200',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    //process start edge
                    selector: 'edge[type=20]',
                    style: {
                        'width': 8,
                        'curve-style': 'bezier',
                        'line-color': "#232323",
                        'target-arrow-color': '#232323',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px",
                        "text-outline-color": "#222222",
                        "text-outline-width": "0.3px"
                    }
                }]
        }
    );

    if (process.data.info != "Inductive Mining") {

        //nodes max time
        let nodesMaxTime = 0;
        for (let i = 0; i < currentProcess.nodeTimes.length; i++) {
            if (currentProcess.nodeTimes[i] > nodesMaxTime) {
                nodesMaxTime = currentProcess.nodeTimes[i];
            }
        }

        process.data.nodesMaxTime = nodesMaxTime;

        //edges max time
        let edgesMaxTime = 0;
        for (let i = 0; i < currentProcess.relationTimes.length; i++) {
            for (let j = 0; j < currentProcess.relationTimes[i].length; j++) {
                if (currentProcess.relationTimes[i][j] > edgesMaxTime) {
                    edgesMaxTime = currentProcess.relationTimes[i][j];
                }
            }
        }
        process.data.edgesMaxTime = edgesMaxTime;

        /* Add time to color scales */
        for (let i = 1; i < 5; i++) {
            document.getElementById(`level-${i}-state`).textContent = timeConvertMinutes((nodesMaxTime / 5) * i)
            document.getElementById(`level-${i}-arrows`).textContent = timeConvertMinutes((edgesMaxTime / 5) * i)
        }

        //add nodes to graph
        console.log(process.data.nodes)
        for (var i = 0; i < process.data.nodes.length; i++) {
            let styleType = Math.floor(currentProcess.nodeTimes[i] * 4 / nodesMaxTime);
            cy.add({
                data: {
                    id: i,
                    label: process.data.nodes[i] + " " + convertToHours(currentProcess.nodeTimes[i]),
                    type: styleType
                }
            }
            );
        }

        //add relations to graph
        for (var i = 0; i < process.data.relations.length; i++) {
            for (var j = 0; j < process.data.relations[i].to.length; j++) {
                let styleType = Math.round(currentProcess.relationTimes[i][j] * 4 / edgesMaxTime);
                cy.add({
                    data: {
                        id: 'relation' + process.data.relations[i].from + '-' + process.data.relations[i].to[j],
                        source: process.data.relations[i].from,
                        target: process.data.relations[i].to[j],
                        label: convertToHours(currentProcess.relationTimes[i][j]),
                        type: styleType
                    }
                });
            }
        }
    } else {
        //INDUCTIVE 
        //add nodes to graph       
        for (var i = 0; i < process.data.nodes.length; i++) {
            cy.add({
                data: {
                    id: i,
                    label: process.data.nodes[i],
                    type: 1
                }
            }
            );
        }

        //add relations to graph
        console.log(process.data.relations.length)
        for (var i = 0; i < process.data.relations.length; i++) {
            for (var j = 0; j < process.data.relations[i].to.length; j++) {
                console.log(process.data.relations[i])
                cy.add({
                    data: {
                        id: 'relation' + process.data.relations[i].from + '-' + process.data.relations[i].to[j],
                        source: process.data.relations[i].from,
                        target: process.data.relations[i].to[j],
                        type: 1
                    }
                });
            }
        }
    }

    //add process start 
    for (let i = 0; i < process.data.startEvents.length; i++) {
        cy.add({
            data: {
                id: 'start-' + i,
                type: 20
            },
        });

        cy.add({
            data: {
                id: 'edge_start-' + i,
                source: 'start-' + i,
                target: process.data.startEvents[i].node,
                type: 20
            }
        });
    }

    //add process end activities
    for (let i = 0; i < process.data.endEvents.length; i++) {
        cy.add({
            data: {
                id: 'end-' + i,
                type: 20
            },
        });

        cy.add({
            data: {
                id: 'edge_end-' + i,
                source: process.data.endEvents[i].node,
                target: 'end-' + i,
                type: 20
            }
        });
    }

    //graph layout
    cy.layout({
        name: 'breadthfirst',
        fit: true, // whether to fit the viewport to the graph
        directed: true, // whether the tree is directed downwards (or edges can point in any direction if false)
        padding: 10, // padding on fit
        circle: false, // put depths in concentric circles if true, put depths top down if false
        grid: false, // whether to create an even grid into which the DAG is placed (circle:false only)
        spacingFactor: 1.3, // positive spacing factor, larger => more space between nodes (N.B. n/a if causes overlap)
        boundingBox: undefined, // constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
        avoidOverlap: true, // prevents node overlap, may overflow boundingBox if not enough space
        nodeDimensionsIncludeLabels: false, // Excludes the label when calculating node bounding boxes for the layout algorithm
        roots: undefined, // the roots of the trees
        maximal: false, // whether to shift nodes down their natural BFS depths in order to avoid upwards edges (DAGS only)
        animate: false, // whether to transition the node positions
        animationDuration: 500, // duration of animation in ms if enabled
        animationEasing: undefined, // easing of animation if enabled,
        animateFilter: function (node, i) {
            return true;
        }, // a function that determines whether the node should be animated.  All nodes animated by default on animate enabled.  Non-animated nodes are positioned immediately when the layout starts
        ready: undefined, // callback on layoutready
        stop: undefined, // callback on layoutstop
        transform: function (node, position) {
            return position;
        } // transform a given node position. Useful for changing flow direction in discrete layouts         
    }).run();

}




/*-----------------Frequency Diagram-------------------*/

function renderFrequencyGraph() {

    console.log("DOING FREQUENCY");
    cy = cytoscape(
        {
            wheelSensitivity: 0.5,
            minZoom: 0.1,
            maxZoom: 1,
            container: document.getElementById('cy'),
            style: [
                {
                    //Nodes styles
                    selector: 'node[type=0]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#FFFFFF",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "text-wrap": "wrap",
                        "text-max-width": "1000px",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=1]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#7f87ff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=2]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#404bff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=3]',
                    style: {
                        "shape": 'round-rectangle',
                        "background-color": "#000eff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    //process start node
                    selector: 'node[type=20]',
                    style: {
                        "shape": 'ellipse',
                        "background-color": "#3f3f3f",
                        "border-width": 4,
                        "border-color": "#131313",
                        'width': '50',
                        "height": "50",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "text-wrap": "wrap",
                        "text-max-width": "1000px",
                        "color": "#FF2222"
                    }
                },

                //Edges styles
                {
                    selector: 'edge[type=0]',
                    style: {
                        'width': 3,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#D1D1D1",
                        'target-arrow-color': '#D1D1D1',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=1]',
                    style: {
                        'width': 5,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#7f87ff",
                        'target-arrow-color': '#7f87ff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=2]',
                    style: {
                        'width': 7,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#404bff",
                        'target-arrow-color': '#404bff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=3]',
                    style: {
                        'width': 9,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#000eff",
                        'target-arrow-color': '#000eff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    //process start edge
                    selector: 'edge[type=20]',
                    style: {
                        'width': 8,
                        'curve-style': 'bezier',
                        'line-color': "#232323",
                        'target-arrow-color': '#232323',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px",
                        "text-outline-color": "#222222",
                        "text-outline-width": "0.3px"
                    }
                },
            ],
        });


    //find max frequency value
    //nodes max frequency
    var maxFrequency = 0;
    for (let i = 0; i < process.data.nodes.length; i++) {
        if (process.data.statistics.nodes[i].frequency > maxFrequency) {
            maxFrequency = process.data.statistics.nodes[i].frequency;
        }
    }

    //edges max frequency
    for (let i = 0; i < process.data.statistics.relations.length; i++) {
        for (let j = 0; j < process.data.statistics.relations[i].to.length; j++) {
            if (process.data.statistics.relations[i].to[j].frequency > maxFrequency) {
                maxFrequency = process.data.statistics.relations[i].to[j].frequency;
            }
        }
    }

    //Nodes\\
    for (let i = 0; i < process.data.nodes.length; i++) {
        let typeValue = Math.round(process.data.statistics.nodes[i].frequency * 3 / maxFrequency);
        //asd[typeValue] = process.data.data.statistics.nodes[i].frequency;
        cy.add({
            data: {
                id: i,
                label: process.data.nodes[i] + ' (' + process.data.statistics.nodes[i].frequency + ')',
                type: typeValue
            },
        }
        );
    }
    process.data.maxFrequency = maxFrequency;

    //Edges\\
    for (let i = 0; i < process.data.relations.length; i++) {
        for (let j = 0; j < process.data.relations[i].to.length; j++) {
            let typeValue = Math.round(process.data.statistics.relations[i].to[j].frequency * 3 / maxFrequency);
            cy.add({
                data: {
                    id: 'edge' + process.data.relations[i].from + '-' + process.data.relations[i].to[j],
                    source: process.data.relations[i].from,
                    target: process.data.relations[i].to[j],
                    name: process.data.statistics.relations[i].to[j].frequency,
                    type: typeValue
                }
            });
        }
    }

    //add process start and end nodes
    for (let i = 0; i < process.data.startEvents.length; i++) {
        cy.add({
            data: {
                id: 'start-' + i,
                type: 20
            },
        });

        cy.add({
            data: {
                id: 'edge_start-' + i,
                source: 'start-' + i,
                target: process.data.startEvents[i].node,
                type: 20
            }
        });
    }

    for (let i = 0; i < process.data.endEvents.length; i++) {
        cy.add({
            data: {
                id: 'end-' + i,
                type: 20
            },
        });

        cy.add({
            data: {
                id: 'edge_end-' + i,
                source: process.data.endEvents[i].node,
                target: 'end-' + i,
                type: 20
            }
        });
    }

    let customBreadthfirst = {
        name: 'breadthfirst',

        fit: true, // whether to fit the viewport to the graph
        directed: true, // whether the tree is directed downwards (or edges can point in any direction if false)
        padding: 10, // padding on fit
        circle: false, // put depths in concentric circles if true, put depths top down if false
        grid: false, // whether to create an even grid into which the DAG is placed (circle:false only)
        spacingFactor: 0.90, // positive spacing factor, larger => more space between nodes (N.B. n/a if causes overlap)
        boundingBox: undefined, // constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
        avoidOverlap: true, // prevents node overlap, may overflow boundingBox if not enough space
        nodeDimensionsIncludeLabels: false, // Excludes the label when calculating node bounding boxes for the layout algorithm
        roots: undefined, // the roots of the trees
        maximal: false, // whether to shift nodes down their natural BFS depths in order to avoid upwards edges (DAGS only)
        animate: false, // whether to transition the node positions
        animationDuration: 500, // duration of animation in ms if enabled
        animationEasing: undefined, // easing of animation if enabled,
        animateFilter: function (node, i) {
            return true;
        }, // a function that determines whether the node should be animated.  All nodes animated by default on animate enabled.  Non-animated nodes are positioned immediately when the layout starts
        ready: undefined, // callback on layoutready
        stop: undefined, // callback on layoutstop
        transform: function (node, position) {
            return position;
        } // transform a given node position. Useful for changing flow direction in discrete layouts
    };

    cy.layout(customBreadthfirst).run();
}



/*-----------------Conformance Diagram-------------------*/

function renderConformanceGraph() {

    console.log("CONFORMANCE");
    cy = cytoscape(
        {
            wheelSensitivity: 0.1,
            minZoom: 0.1,
            maxZoom: 1,
            container: document.getElementById('cy'),
            style: [
                {
                    //Nodes styles
                    selector: 'node[type=0]',
                    style: {
                        "padding-relative-to": "width",
                        "shape": 'round-rectangle',
                        "background-color": "#77E543",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "text-wrap": "wrap",
                        "text-max-width": "1000px",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=1]', //red
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#acbcff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=2]', //green
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#748fff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                }, 
                {
                    selector: 'node[type=3]', //yellow
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#365eff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 3,
                        "border-color": "#484848",
                        "font-size": "18px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=4]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#0032ff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#FFFFFF"
                    }
                },
                {
                    //process start node
                    selector: 'node[type=20]',
                    style: {
                        "shape": 'ellipse',
                        "background-color": "#3f3f3f",
                        "border-width": 4,
                        "border-color": "#131313",
                        'width': '50',
                        "height": "50",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "text-wrap": "wrap",
                        "text-max-width": "1000px",
                        "color": "#FF2222"
                    }
                },

                //Edges styles
                {
                    selector: 'edge[type=0]',
                    style: {
                        'width': 3,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#D1D1D1",
                        'target-arrow-color': '#D1D1D1',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=1]',
                    style: {
                        'width': 5,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#acbcff",
                        'target-arrow-color': '#acbcff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=2]',
                    style: {
                        'width': 7,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#748fff",
                        'target-arrow-color': '#748fff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    selector: 'edge[type=3]',
                    style: {
                        'width': 9,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#365eff",
                        'target-arrow-color': '#365eff',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px"
                    }
                },
                {
                    //process start edge
                    selector: 'edge[type=20]',
                    style: {
                        'width': 8,
                        'curve-style': 'bezier',
                        'line-color': "#232323",
                        'target-arrow-color': '#232323',
                        "font-size": "32px",
                        "color": "#222222",
                        "loop-direction": "0deg",
                        'target-arrow-shape': 'triangle',
                        "loop-sweep": "45deg",
                        "text-margin-y": "-15px",
                        "source-text-offset": "50px",
                        "text-outline-color": "#222222",
                        "text-outline-width": "0.3px"
                    }
                },
            ],
        });



    //Nodes

    let nodes = process.base.nodes;
    nodes.forEach(function (node, index) {
        let typeValue = 0

        for (let k = 0; k < process.case.nodes.length; k++) { // Verde
            if (process.case.nodes[k].node === index) {
                typeValue = 1
            }
        }

        //Verificar se tempo execução é não conforme -> texto vermelho
        //Procurar a ocorrência do nó em questão com maior duração (pior caso)
        let maxDurationCase = {}
        maxDurationCase.days = 
        maxDurationCase.hours = 0
        maxDurationCase.minutes = 0
        maxDurationCase.seconds = 0
      
        for (let k = 0; k < process.case.nodes.length; k++) {
            if (process.case.nodes[k].node === index && convertToSeconds(process.case.nodes[k].duration) > convertToSeconds(maxDurationCase)) {
                maxDurationCase = process.case.nodes[k].duration
            }
        }

        if (convertToSeconds(process.base.taskDurations[index].duration) > convertToSeconds(maxDurationCase) * 1.10) {
            typeValue = 2; // Vermelho
        } else if (convertToSeconds(process.base.taskDurations[index].duration) >= convertToSeconds(maxDurationCase) * 0.9 && convertToSeconds(process.base.taskDurations[index].duration) <= convertToSeconds(maxDurationCase) * 1.10) {
            typeValue = 3; // Amarelinho
        }

        let processLabel = "\n Processo: " + durationToString(process.base.taskDurations[index].duration);

        let modeloLabel = "Modelo: " + durationToString(maxDurationCase);

        cy.add({
            data: {
                id: index,
                label: processLabel + " / " + modeloLabel,
                type: typeValue

            },
        }
        );
    });

    //edges
    for (let i = 0; i < process.base.relations.length; i++) {
        for (let j = 0; j < process.base.relations[i].to.length; j++) {
            //determinar cor do nó
            let typeValue = 0;

            //let prevLabel = "\n Modelo: " + convertToHours(process.data.nodes.taskDurations[i].duration);

            cy.add({
                data: {
                    id: 'edge' + process.base.relations[i].from + '-' + process.base.relations[i].to[j].node,
                    source: process.base.relations[i].from,
                    target: process.base.relations[i].to[j].node,
                    //label: prevLabel + " / " + realLabel,                 
                }
            });
        }
    }

    //n funciona :(
    //edges = process.data.nodes;
    //edges.forEach(function (node, i) {
    //    console.log(process.data.relations[i].to)
    //    process.data.relations[i].to.forEach(function (node, j) {

    //        let typeValue = 0;

    //        cy.add({
    //            data: {
    //                id: 'edge' + process.data.relations[i].from + '-' + process.data.relations[i].to[j].node,
    //                source: process.data.relations[i].from,
    //                target: process.data.relations[i].to[j].node,
    //                label: prevLabel + " / " + realLabel,                 
    //            }
    //        });

    //    });
    //});


    //starting nodes
    let startEvents = process.base.startEvents;
    startEvents.forEach(function (node, i) {
        cy.add({
            data: {
                id: 'start-' + i,
                type: 20
            },
        });

        cy.add({
            data: {
                id: 'edge_start-' + i,
                source: 'start-' + i,
                target: process.base.startEvents[i].node,
                type: 20
            }
        });
    });



    //end nodes
    for (let i = 0; i < process.base.endEvents.length; i++) {
        cy.add({
            data: {
                id: 'end-' + i,
                type: 20
            },
        });
        cy.add({
            data: {
                id: 'edge_end-' + i,
                source: process.base.endEvents[i].node,
                target: 'end-' + i,
                type: 20
            }
        });
    }



    let customBreadthfirst = {
        name: 'breadthfirst',

        fit: true, // whether to fit the viewport to the graph
        directed: true, // whether the tree is directed downwards (or edges can point in any direction if false)
        padding: 10, // padding on fit
        circle: false, // put depths in concentric circles if true, put depths top down if false
        grid: false, // whether to create an even grid into which the DAG is placed (circle:false only)
        spacingFactor: 0.90, // positive spacing factor, larger => more space between nodes (N.B. n/a if causes overlap)
        boundingBox: undefined, // constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
        avoidOverlap: true, // prevents node overlap, may overflow boundingBox if not enough space
        nodeDimensionsIncludeLabels: false, // Excludes the label when calculating node bounding boxes for the layout algorithm
        roots: undefined, // the roots of the trees
        maximal: false, // whether to shift nodes down their natural BFS depths in order to avoid upwards edges (DAGS only)
        animate: false, // whether to transition the node positions
        animationDuration: 500, // duration of animation in ms if enabled
        animationEasing: undefined, // easing of animation if enabled,
        animateFilter: function (node, i) {
            return true;
        }, // a function that determines whether the node should be animated.  All nodes animated by default on animate enabled.  Non-animated nodes are positioned immediately when the layout starts
        ready: undefined, // callback on layoutready
        stop: undefined, // callback on layoutstop
        transform: function (node, position) {
            return position;
        } // transform a given node position. Useful for changing flow direction in discrete layouts
    };

    cy.layout(customBreadthfirst).run();
}



function convertToSeconds(duration) {
    console.log(duration)
    return duration.days * 86400 + duration.hours * 3600 + duration.minutes * 60 + duration.seconds;
}


function convertToHours(duration) {


    let hours = (duration / 60);
    let rhours = Math.floor(hours);

    let min = (hours - rhours) * 60;
    let rminutes = Math.round(min);

    if (rhours == 0) {
        return rminutes + " min";
    }
    return rhours + "h " + rminutes + " min";
}


function timeConvertMinutes(minAux) {
    let hours = minAux / 60
    let hoursFloor = Math.floor(hours)
    let min = (hours - hoursFloor) * 60

    return min < 1 && min > 0 ? `${(min * 60).toFixed(3)} segundos` : `${hoursFloor}h ${min.toFixed(0)}min`
}


function durationToString(duration) {
    if (duration.days == 0 && duration.hours == 0 && duration.minutes == 0 && duration.seconds == 0) {
        return "0S";
    }
    return ((duration.days !== 0) ? duration.days + "D " : "") +
        ((duration.hours !== 0) ? duration.hours + "H " : "") +
        ((duration.minutes !== 0) ? duration.minutes + "M " : "") +
        ((duration.seconds !== 0) ? duration.seconds + "S" : "");
}