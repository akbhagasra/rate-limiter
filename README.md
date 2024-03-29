# Rate Limiter

Implementation of various api rate limiter methods. Refer to coding challenge [Build Your Own Rate Limiter](https://codingchallenges.fyi/challenges/challenge-rate-limiter/)

## Token Bucket

The Token Bucket algorithm works by maintaining a bucket of tokens, where each token represents the permission to process one request. Requests are allowed to be processed only if there are available tokens in the bucket. Tokens are added to the bucket at a fixed rate.

### Implementation:

- The implementation maintains a token bucket with a maximum capacity, a refill rate and the last updated time of the bucket.
- Each time a request is received, the tokens are updated in the bucket based on the last updated time and request is fulfilled based on the current available tokens.

## Fixed Window Counter

Fixed Window Counter allows a fixed number of requests to be processed within a specified time window. It counts the number of requests received within the window and resets the counter at the beginning of each window.

### Implementation:

- The implementation maintains a counter for the number of requests received within the current window.
- Each time a request is received the counter is updated based on the current window and the request is fulfilled if counter is less than the capacity.

## Sliding Window Counter

Sliding Window Counter maintains a window of fixed duration and tracks the number of requests received within that window. Unlike Fixed Window Counter, the window moves with time instead of being fixed to specific intervals.

### Implementation:

- The implementation maintains a counter for the number of requests received within the sliding window and a queue to keep track of requests received within the current window.
- Each time a request is received, the queue is updated based on the current time window and request is fulfilled if there is capacity
