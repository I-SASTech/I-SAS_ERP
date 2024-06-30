<template>

    <div class="row m-4">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <div class="row ml-1">
                    </div>
                    <div class="row">
                        <input type="file" accept="image/*" @change="onChange" />
                        <div id="preview">
                            <img v-if="item.imageUrl" :src="item.imageUrl" width="300" height="300" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data: function () {
            return {
                item: {
                    image: null,
                    imageUrl: null
                }
            }
        },
        methods: {
            onChange(e) {
                const file = e.target.files[0]
                this.image = file
                this.item.imageUrl = URL.createObjectURL(file)
            }
        },
    }
</script>
