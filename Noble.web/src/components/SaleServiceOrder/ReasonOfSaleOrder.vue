<template>
    <modal :show="show">
        <div class="modal-content border">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">Reason</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label>Why you close this?</label>
                        <div>
                            <textarea rows="5" class="form-control" v-model="reason" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="Save()" >Save</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">Close</button>
            </div>
        </div>
    </modal>
</template>
<script>
    export default {

        props: ['show', 'type'],
        components: {

        },
        data: function () {
            return {
                rander: 0,
                reason: '',
                
            }
        },
        validations() {
           
        },
        methods: {
            Save: function () {
                this.$emit('SaveRecord',this.reason);
            },
            close: function () {
                this.$emit('close');
            },
           
           
        },
        mounted: function () {

        }
    }
</script>
