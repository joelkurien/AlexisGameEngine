#include<iostream>

using namespace std;

struct Chunk {
    Chunk *next;
};

//Bump-Pool Allocator (Block-Chunk Linked List algorithm)
class ParticleAllocator {
    private:
        size_t mChunksPerBlock;
        Chunk *pos = nullptr;
        Chunk *allocateBlock(size_t chunkSize);
    public:
        ParticleAllocator(size_t sizeOfChunks) : mChunksPerBlock(sizeOfChunks){}

        void *allocate(size_t chunkSize);
        void deallocate(void *ptr, size_t chunkSize);
};

Chunk *ParticleAllocator::allocateBlock(size_t chunkSize) {
    cout<<"Size of each chunk: " + mChunksPerBlock<<endl;
    int blockSize = mChunksPerBlock * chunkSize;
    Chunk *startBlock = reinterpret_cast<Chunk*>(malloc(blockSize));
    Chunk *currentChunk = startBlock;
    int cnt = 0;
    while(currentChunk->next != nullptr){
        currentChunk->next = reinterpret_cast<Chunk*>(reinterpret_cast<char*>(currentChunk) + chunkSize);
        currentChunk = currentChunk->next;
        cnt++;
    }
    if(cnt == mChunksPerBlock) { cout<<"Valid Loop\n"; }
    currentChunk->next = nullptr;
    return startBlock;
}

void *ParticleAllocator::allocate(size_t chunkSize){
    if(pos == nullptr){
        pos = allocateBlock(chunkSize);
    }

    Chunk* freeChunk = pos;
    pos->next = nullptr;
    return freeChunk;
}

void ParticleAllocator::deallocate(void *chunk, size_t chunkSize){
    Chunk* free = reinterpret_cast<Chunk*>(chunk);
    free->next = pos;
    pos = free;
}
