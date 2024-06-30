<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> {{ $t('HoldModel.HoldInvoices') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row ">
                    <div class="form-group col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('HoldModel.RefrenceNo') }}:</label>
                        <input class="form-control" v-shortkey.avoid v-model="holdModel.refrenceNo" type="text" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveOrigin">{{ $t('HoldModel.Save') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('HoldModel.Close') }}</button>
            </div>
        </div>
    </modal>
   
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        mixins: [clickMixin],
        props: ['show','sale' ],
        data: function () {
            return {               
                holdModel: {}
              
            }
        },
        methods: {
            close: function () {
                    this.$emit('close', false);
            },         
            SaveOrigin: function () {
                this.$emit('saveSale', this.holdModel);               
                this.close();
            }
        },
        created: function () {
            this.holdModel = this.sale;
        },
        mounted: function () {
        }
    }
</script>
