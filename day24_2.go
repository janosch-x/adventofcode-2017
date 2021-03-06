package main

import (
	"fmt"
	"strconv"
	"strings"
)

func main() {
	lines := strings.Split(input(), "\n")
	ports := make([][]int, 0, len(lines))

	for _, line := range lines {
		nums := strings.Split(line, "/")
		one, _ := strconv.Atoi(nums[0])
		two, _ := strconv.Atoi(nums[1])
		ports = append(ports, []int{one, two})
	}

	maxLength := 0
	longest := make([][]int, 0)

	callback := func(builtBridge []int) {
		length := len(builtBridge)
		if length > maxLength {
			maxLength = length
			longest = make([][]int, 0)
		}
		if length >= maxLength {
			longest = append(longest, builtBridge)
		}
	}

	buildBridges(make([]int, 0), ports, 0, callback)

	max := 0
	for _, bridge := range longest {
		sum := 0
		for _, num := range bridge {
			sum += num
		}
		if sum > max {
			max = sum
		}
	}

	fmt.Printf("max: %d\n", max)
}

func buildBridges(bridge []int, ports [][]int, next int, callback func([]int)) {
	callback(bridge)
	for i, port := range ports {
		if contains(port, next) {
			newNext := port[0]
			if port[0] == next {
				newNext = port[1]
			}
			otherPorts := make([][]int, len(ports))
			copy(otherPorts, ports)
			otherPorts[i] = otherPorts[len(ports)-1] // Replace it with the last one.
			otherPorts = otherPorts[:len(ports)-1]   // Chop off the last one.
			buildBridges(append(bridge, port...), otherPorts, newNext, callback)
		}
	}
}

func contains(port []int, num int) bool {
	for _, el := range port {
		if el == num {
			return true
		}
	}
	return false
}

func input() string {
	return `... paste input here without indentation ...`
}
