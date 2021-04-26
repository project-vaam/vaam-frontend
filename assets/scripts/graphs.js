
var cy = null

var currentProcess = {
    nodeTimes: [],
    relationTimes: [],
    nodesMaxTime: '',
    edgesMaxTime: '',
};


// var process = <%=processes%>; //gets the selected process and his data from asp net codebehind   

//console.log(process);

function generateGraph(process) {

    console.log(process);

    if (process.isPerformance === "True") {
        console.log("performance");
        //nodes timers
        let nodeTimes = [];
        for (let i = 0; i < process.data.statistics.nodes.length; i++) {
            let meanDuration = process.data.statistics.nodes[i].meanDuration.days + '.' + process.data.statistics.nodes[i].meanDuration.hours + ':' +
                process.data.statistics.nodes[i].meanDuration.minutes + ':' + process.data.statistics.nodes[i].meanDuration.seconds + '.' + process.data.statistics.nodes[i].meanDuration.millis;

            nodeTimes[i] = moment.duration(meanDuration).asMinutes();

        }
        currentProcess.nodeTimes = nodeTimes;
        console.log("performanceII");
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

        renderPerformanceGraph();
    } else {
        console.log("frequency");
        renderFrequencyGraph();
    }
}









/*-----------------Performance Diagram*-------------------*/


function renderPerformanceGraph() {
    console.log("performanceIII");
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
                        "shape": 'rectangle',
                        "background-color": "#fff0d9",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=1]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#ffce89",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=2]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#ff9457",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=3]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#f25831",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=4]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#be1300",
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

    console.log("performanceIV");

    //find max time
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
    for (var i = 0; i < process.data.nodes.length; i++) {
        let styleType = Math.floor(currentProcess.nodeTimes[i] * 4 / nodesMaxTime);
        cy.add({
            data: {
                id: i,
                label: process.data.nodes[i] + ' (' + durationToString(currentProcess.nodeTimes[i]) + ')',
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
                    label: durationToString(currentProcess.relationTimes[i][j]),
                    type: styleType
                }
            });
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
                        "shape": 'rectangle',
                        "background-color": "#FFFFFF",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
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
                        "shape": 'rectangle',
                        "background-color": "#acbcff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=2]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#748fff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
                        "text-valign": "center",
                        "text-halign": "center",
                        "color": "#222222"
                    }
                },
                {
                    selector: 'node[type=3]',
                    style: {
                        "shape": 'rectangle',
                        "background-color": "#365eff",
                        "label": "data(label)",
                        'width': '350',
                        "height": "40",
                        "border-width": 2,
                        "border-color": "#484848",
                        "font-size": "16px",
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
                    selector: 'edge[type=4]',
                    style: {
                        'width': 13,
                        'curve-style': 'bezier',
                        "content": "data(name)",
                        "line-color": "#0032ff",
                        'target-arrow-color': '#0032ff',
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
        let typeValue = Math.round(process.data.statistics.nodes[i].frequency * 4 / maxFrequency);
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
            let typeValue = Math.round(process.data.statistics.relations[i].to[j].frequency * 4 / maxFrequency);
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



function durationToString(duration) {
    if (duration == 0) {
        durationToString
        return "0S";
    }

    return ((moment.duration({ "minutes": duration }).days() !== 0) ? moment.duration({ "minutes": duration }).days() + "D " : "") +
        ((moment.duration({ "minutes": duration }).hours() !== 0) ? moment.duration({ "minutes": duration }).hours() + "H " : "") +
        ((moment.duration({ "minutes": duration }).minutes() !== 0) ? moment.duration({ "minutes": duration }).minutes() + "M " : "") +
        ((moment.duration({ "minutes": duration }).seconds() !== 0) ? moment.duration({ "minutes": duration }).seconds() + "S" : "");
}

function timeConvertMinutes(minAux) {
    let hours = minAux / 60
    let hoursFloor = Math.floor(hours)
    let min = (hours - hoursFloor) * 60

    return min < 1 && min > 0 ? `${(min * 60).toFixed(3)} segundos` : `${hoursFloor}h ${min.toFixed(0)}min`
}

